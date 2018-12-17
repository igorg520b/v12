#include"PPR_math.h"

// node indices
#define X0_OFFSET 0
#define UN_OFFSET 3
#define VN_OFFSET 6
#define AN_OFFSET 9
#define F_OFFSET 12
#define X_CURRENT_OFFSET 15
#define FP_DATA_SIZE_NODE 18

// CZ fp data
#define CURRENT_PMAX_OFFSET_CZ 0
#define CURRENT_TMAX_OFFSET_CZ 3
#define TENTATIVE_PMAX_OFFSET_CZ 6
#define TENTATIVE_TMAX_OFFSET_CZ 9
// additional variables that preserve traction-separation
#define DELTA_N_OFFSET_CZ 12
#define DELTA_T_OFFSET_CZ 13
#define T_N_OFFSET_CZ 14
#define T_T_OFFSET_CZ 15
#define FP_DATA_SIZE_CZ 16

// CZ integer data
#define CURRENT_FAILED_OFFSET_CZ 0
#define TENTATIVE_CONTACT_OFFSET_CZ 1
#define TENTATIVE_DAMAGED_OFFSET_CZ 2
#define TENTATIVE_FAILED_OFFSET_CZ 3
#define VRTS_OFFSET_CZ 4
#define PCSR_OFFSET_CZ 10 // 36+6=42 values
#define ROWSIZE_OFFSET_CZ 52 
#define INT_DATA_SIZE_CZ 58



// (5) thermodynamically consistent
__device__ void cohesive_law_thermodynamically_consistent(bool &cz_contact, bool &cz_failed, double &pmax, double &tmax, double opn, double opt,
	double &Tn, double &Tt, double &Dnn, double &Dtt, double &Dnt, double &Dtn) {
	Tn = Tt = Dnn = Dtt = Dnt = Dtn = 0;
	if (opn > deln || opt > delt) {
		cz_failed = true; return;
	}
	cz_contact = (opn < 0);
	const double epsilon = -1e-9;
	const double epsilon2 = 0.05; // if traction is <5% of max, CZ fails
	double threshold_tangential = f_tt * epsilon2;
	double threshold_normal = f_tn * epsilon2;

	if (cz_contact)
	{
		Dnt = 0;
		if (pmax != 0) {
			double peakTn = Tn_(pmax, tmax);
			Tn = peakTn * opn / pmax;
			Dnn = peakTn / pmax;
		}
		else {
			Dnn = Dnn_(0, tmax);
			Tn = Dnn * opn;
		}

		Tt = Tt_(0, opt);
		if (Tt >= epsilon && !(opt > delt*rt/5 && Tt < threshold_tangential))
		{
			if (opt >= tmax)
			{
				// tangential softening
				tmax = opt;
				Dtt = Dtt_(0, opt);
			}
			else
			{
				// unload/reload
				double peakTt = Tt_(0, tmax);
				Tt = peakTt * opt / tmax;
				Dtt = peakTt / tmax;
			}

		}
		else
		{
			// cz failed in tangential direction while in contact
			Tt = Dtt = Dnt = 0;
			Tn = Dnn = 0;
			cz_failed = true;
		}
	}
	else
	{
		// not in contact
		Tt = Tt_(opn, opt);
		Tn = Tn_(opn, opt);
		if (Tt >= epsilon && Tn >= epsilon &&
			!(opt > delt*rt/5 && Tt < threshold_tangential) &&
			!(opn > deln*rn/5 && Tn < threshold_normal))
		{
			// tangential component
			bool tsoft = (opt >= tmax);
			bool nsoft = (opn >= pmax);
			if (tsoft && nsoft)
			{
				// tangential and normal softening
				tmax = opt;
				pmax = opn;
				Dnn = Dnn_(opn, opt);
				Dnt = Dnt_(opn, opt);
				Dtt = Dtt_(opn, opt);
			}
			else if (tsoft && !nsoft) {
				Dnt = 0;
				if (pmax != 0) {
					double peakTn = Tn_(pmax, tmax);
					Tn = peakTn * opn / pmax;
					Dnn = peakTn / pmax;
				}
				else {
					Tn = 0; Dnn = Dnn_(0, tmax);
				}

				// normal unload/reload
				tmax = opt;
				Tt = Tt_(pmax, opt);
				Dtt = Dtt_(pmax, opt);
			}
			else if (!tsoft && nsoft)
			{
				Dnt = 0;
				if (tmax != 0) {
					double peakTt = Tt_(pmax, tmax);
					Tt = peakTt * opt / tmax;
					Dtt = peakTt / tmax;
				}
				else {
					Tt = 0; Dtt = Dtt_(pmax, 0);
				}

				pmax = opn;
				Tn = Tn_(pmax, tmax);
				Dnn = Dnn_(pmax, tmax);

			}
			else
			{
				Dnt = 0;
				// reloading in both tangential and normal
				double peakTn = Tn_(pmax, tmax);
				if (pmax != 0)
				{
					Tn = peakTn * opn / pmax;
					Dnn = peakTn / pmax;
				}
				else {
					Tn = 0; Dnn = Dnn_(0, tmax);
				}

				if (tmax != 0) {
					double peakTt = Tt_(pmax, tmax);
					Tt = peakTt * opt / tmax;
					Dtt = peakTt / tmax;
				}
				else {
					Tt = 0; Dtt = Dtt_(pmax, 0);
				}
			}

		}
		else
		{
			cz_failed = true;
			Tn = Tt = Dnn = Dtt = Dnt = 0;
		}
	}
	Dtn = Dnt;
}

// assembling routines
__device__ void AssembleBCSR18(const int *pcsr, double *A, double *b, const int stride,
	double(&LHS)[18][18], double(&rhs)[18]) {

	// BCSR format
	// distribute LHS into global matrix, and rhs into global rhs
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			int idx1 = pcsr[stride * (i * 6 + j)];
			if (idx1 >= 0)
			{
				// write into csr.vals
				for (int k = 0; k < 3; k++)
					for (int l = 0; l < 3; l++)
						atomicAdd2(&A[idx1 * 9 + 3 * k + l], LHS[i * 3 + k][j * 3 + l]);
			}
		}
		// distribute rhs
		int idx2 = pcsr[stride * (36 + i)];
		if (idx2 >= 0)
		{
			for (int k = 0; k < 3; k++)
				atomicAdd2(&b[idx2 * 3 + k], rhs[i * 3 + k]);
		}
	}
}


// dcz: double cz data
// icz: integer cz data (includes pcsr)
// dnd: double nodal data
// h: timestep
extern "C" __global__ void kczCZForce(
	double *dcz, int *icz,
	double *dnd,
	double *_global_matrix, double *_global_rhs,
	const double h,
	const int nCZs, const int cz_stride, const int nd_stride) 
{
	int idx = threadIdx.x + blockIdx.x * blockDim.x;
	if (idx >= nCZs) return;
	if (icz[idx + cz_stride*(CURRENT_FAILED_OFFSET_CZ)] != 0) return; // cz failed

	double x0[18], un[18];
	double xc[18], xr[18]; // xc = x0 + un; xr = R xc;  *in this case R is the inverse rotation
	double pmax[3], tmax[3];

	// retrieve nodal data from the global memory
	for (int i = 0; i < 6; i++) {
		int vrtx = icz[idx + cz_stride * (VRTS_OFFSET_CZ + i)];
		for (int j = 0; j < 3; j++) {
			int idx1 = i * 3 + j;
			x0[idx1] = dnd[vrtx + nd_stride * (X0_OFFSET + j)];
			un[idx1] = dnd[vrtx + nd_stride * (UN_OFFSET + j)];
			xc[idx1] = x0[idx1] + un[idx1];
		}
	}

	// retrieve the cz damage state
	for (int i = 0; i < 3; i++) {
		pmax[i] = dcz[idx + cz_stride*(CURRENT_PMAX_OFFSET_CZ + i)];
		tmax[i] = dcz[idx + cz_stride*(CURRENT_TMAX_OFFSET_CZ + i)];
	}

	// find the midplane 
	double mpc[9]; // midplane coordinates
	for (int i = 0; i < 9; i++) mpc[i] = (xc[i] + xc[i + 9])*0.5;

	// find the rotation of the midplane
	double R[3][3];
	double a_Jacob;
	CZRotationMatrix(
		mpc[0], mpc[1], mpc[2],
		mpc[3], mpc[4], mpc[5],
		mpc[6], mpc[7], mpc[8],
		R[0][0], R[0][1], R[0][2],
		R[1][0], R[1][1], R[1][2],
		R[2][0], R[2][1], R[2][2],
		a_Jacob);

	// compute the coordinates xr in the local system
	for (int i = 0; i < 6; i++)
		multAX(
			R[0][0], R[0][1], R[0][2],
			R[1][0], R[1][1], R[1][2],
			R[2][0], R[2][1], R[2][2],
			xc[i * 3 + 0], xc[i * 3 + 1], xc[i * 3 + 2],
			xr[i * 3 + 0], xr[i * 3 + 1], xr[i * 3 + 2]
			);

	// total over all gauss points
	double Keff[18][18] = {};
	double rhs[18] = {};

	bool cz_contact_gp[3] = {};
	bool cz_failed_gp[3] = {};

	double avgDn, avgDt, avgTn, avgTt; // preserve average traction-separations for analysis
	avgDn = avgDt = avgTn = avgTt = 0;
	// loop over 3 Gauss points
	for (int gpt = 0; gpt < 3; gpt++)
	{
		// shear and normal local opening displacements
		double dt1, dt2, dn;
		dt1 = dt2 = dn = 0;
		for (int i = 0; i < 3; i++)
		{
			dt1 += (xr[i * 3 + 0] - xr[i * 3 + 9]) * sf[i][gpt];
			dt2 += (xr[i * 3 + 1] - xr[i * 3 + 10]) * sf[i][gpt];
			dn += (xr[i * 3 + 2] - xr[i * 3 + 11]) * sf[i][gpt];
		}
		double opn = dn;
		double opt = sqrt(dt1 * dt1 + dt2 * dt2);

		double Tn, Tt, Dnn, Dtt, Dnt, Dtn;
		cohesive_law_thermodynamically_consistent(cz_contact_gp[gpt], cz_failed_gp[gpt], pmax[gpt], tmax[gpt], opn, opt, Tn, Tt, Dnn, Dtt, Dnt, Dtn);

		// preserve average traction-separations for analysis
		avgDn += opn / 3;
		avgDt += opt / 3;
		avgTn += Tn / 3;
		avgTt += Tt / 3;

		double T[3] = {};
		double T_d[3][3] = {};

		if (opt < 1e-20)
		{
			T[2] = Tn;
			T_d[0][0] = Dtt;
			T_d[1][1] = Dtt;
			T_d[2][2] = Dnn;

			T_d[1][0] = T_d[0][1] = 0;

			T_d[2][0] = Dtn;
			T_d[0][2] = Dnt;
			T_d[2][1] = Dtn;
			T_d[1][2] = Dnt;
		}
		else
		{
			T[0] = Tt * dt1 / opt;
			T[1] = Tt * dt2 / opt;
			T[2] = Tn;

			double opt_sq = opt * opt;
			double opt_cu = opt_sq * opt;
			double delu00 = dt1 * dt1;
			double delu10 = dt2 * dt1;
			double delu11 = dt2 * dt2;

			T_d[0][0] = Dtt * delu00 / opt_sq + Tt * delu11 / opt_cu;
			T_d[1][1] = Dtt * delu11 / opt_sq + Tt * delu00 / opt_cu;
			T_d[2][2] = Dnn;

			T_d[1][0] = T_d[0][1] = Dtt * delu10 / opt_sq - Tt * delu10 / opt_cu;

			T_d[2][0] = Dtn * dt1 / opt;
			T_d[0][2] = Dnt * dt1 / opt;
			T_d[2][1] = Dtn * dt2 / opt;
			T_d[1][2] = Dnt * dt2 / opt;
		}

		// RHS
		// BtT = Bt x T x (-GP_W)
		const double GP_W = 1.0 / 3.0; // Gauss point weight
		double BtT[18] = {};
		for (int i = 0; i < 18; i++) {
			for (int j = 0; j < 3; j++) {
				BtT[i] += B[gpt][j][i] * T[j];
			}
			BtT[i] *= -(GP_W*a_Jacob);
		}

		// rotate BtT
		double rhs_gp[18] = {};
		for (int i = 0; i < 6; i++) {
			multAX(R[0][0], R[1][0], R[2][0],
				R[0][1], R[1][1], R[2][1],
				R[0][2], R[1][2], R[2][2],
				BtT[i * 3 + 0], BtT[i * 3 + 1], BtT[i * 3 + 2],
				rhs_gp[i * 3 + 0], rhs_gp[i * 3 + 1], rhs_gp[i * 3 + 2]);
		}

		// add to rhs
		for (int i = 0; i < 18; i++) rhs[i] += rhs_gp[i];

		// STIFFNESS MATRIX
		// compute Bt x T_d x GP_W
		double BtTd[18][3] = {};
		for (int row = 0; row < 18; row++)
			for (int col = 0; col < 3; col++) {
				for (int k = 0; k < 3; k++) BtTd[row][col] += B[gpt][k][row] * T_d[k][col];
				BtTd[row][col] *= (GP_W*a_Jacob);
			}

		// BtTdB = BtTd x B
		double BtTdB[18][18] = {};
		for (int row = 0; row < 18; row++)
			for (int col = 0; col < 18; col++)
				for (int k = 0; k < 3; k++)
					BtTdB[row][col] += BtTd[row][k] * B[gpt][k][col];

		double TrMtBtTdB[18][18] = {};

		// Keff
		for (int i = 0; i < 6; i++)
			for (int j = 0; j < 6; j++)
			{
				// TrMtBtTdB = TrMt x BtTdB
				for (int k = 0; k < 3; k++)
					for (int l = 0; l < 3; l++) {
						for (int m = 0; m < 3; m++)
							TrMtBtTdB[3 * i + k][3 * j + l] += R[m][k] * BtTdB[3 * i + m][3 * j + l];
					}

				// Keff = TrMt x BtTdB x TrM
				for (int k = 0; k < 3; k++)
					for (int l = 0; l < 3; l++) {
						for (int m = 0; m < 3; m++)
							Keff[3 * i + k][3 * j + l] += TrMtBtTdB[3 * i + k][3 * j + m] * R[m][l];
					}
			}
	}

	// copy tentative values to global memory
	int damaged = 0;
	// the following approach to pmax, tmax is somewhat experimental
	double pmax_ = max(max(pmax[0], pmax[1]), pmax[2]);
	double tmax_ = max(max(tmax[0], tmax[1]), tmax[2]);

	for (int i = 0; i < 3; i++) {
		//		dcz[idx + cz_stride*(TENTATIVE_PMAX_OFFSET_CZ + i)] = pmax[i];
		//		dcz[idx + cz_stride*(TENTATIVE_TMAX_OFFSET_CZ + i)] = tmax[i];
		dcz[idx + cz_stride*(TENTATIVE_PMAX_OFFSET_CZ + i)] = pmax_;
		dcz[idx + cz_stride*(TENTATIVE_TMAX_OFFSET_CZ + i)] = tmax_;
		if (pmax[i] >= deln*rn || tmax[i] >= delt*rt) damaged = 1;
	}
	// finally, cz_contact and _cz_failed
	bool cz_failed = cz_failed_gp[0] || cz_failed_gp[1] || cz_failed_gp[2];
	bool cz_contact = cz_contact_gp[0] || cz_contact_gp[1] || cz_contact_gp[2];
	icz[idx + cz_stride*TENTATIVE_CONTACT_OFFSET_CZ] = cz_contact ? 1 : 0;
	icz[idx + cz_stride*TENTATIVE_FAILED_OFFSET_CZ] = cz_failed ? 1 : 0;
	icz[idx + cz_stride*TENTATIVE_DAMAGED_OFFSET_CZ] = cz_failed ? 0 : damaged;

	if (cz_failed) return;

	// distribute K and rhs into CSR
	AssembleBCSR18(&icz[idx + cz_stride*PCSR_OFFSET_CZ], _global_matrix, _global_rhs, cz_stride, Keff, rhs);

	// preserve avgDn, avgDt, avgTn, avgTt
	dcz[idx + cz_stride*DELTA_N_OFFSET_CZ] = avgDn;
	dcz[idx + cz_stride*DELTA_T_OFFSET_CZ] = avgDt;
	dcz[idx + cz_stride*T_N_OFFSET_CZ] = avgTn;
	dcz[idx + cz_stride*T_T_OFFSET_CZ] = avgTt;
}
