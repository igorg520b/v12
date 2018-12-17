#pragma once
#include"GteSymmetricEigensolver3x3.h"

using namespace System;
using namespace gte;

namespace GeometricTools {

	public ref class GT
	{
	private:
		SymmetricEigensolver3x3<double> *solver;

	public:
		GT() {
			solver = new SymmetricEigensolver3x3<double>();
		}
		!GT() {
			if (solver != nullptr) delete solver;
		}

		void Eigenvalues(array<double>^ matrix, array<double>^ eigenvalues) {
			std::array<double, 3> eval;
			std::array<std::array<double, 3>, 3> evec;
			double xx, yy, zz, xy, yz, zx;
			xx = matrix[0];
			yy = matrix[1];
			zz = matrix[2];
			xy = matrix[3];
			yz = matrix[4];
			zx = matrix[5];
			double a00, a01, a02, a11, a12, a22;
			a00 = xx;
			a01 = xy;
			a02 = zx;
			a11 = yy;
			a12 = yz;
			a22 = zz;
			(*solver)(a00, a01, a02, a11, a12, a22, false, -1, eval, evec);
			eigenvalues[0] = eval[0];
			eigenvalues[1] = eval[1];
			eigenvalues[2] = eval[2];
		}
	};
}
