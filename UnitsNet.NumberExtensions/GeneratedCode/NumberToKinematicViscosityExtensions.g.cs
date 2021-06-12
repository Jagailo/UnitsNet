﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by \generate-code.bat.
//
//     Changes to this file will be lost when the code is regenerated.
//     The build server regenerates the code before each build and a pre-build
//     step will regenerate the code on each local build.
//
//     See https://github.com/angularsen/UnitsNet/wiki/Adding-a-New-Unit for how to add or edit units.
//
//     Add CustomCode\Quantities\MyQuantity.extra.cs files to add code to generated quantities.
//     Add UnitDefinitions\MyQuantity.json and run generate-code.bat to generate new units or quantities.
//
// </auto-generated>
//------------------------------------------------------------------------------

// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System;

#nullable enable

namespace UnitsNet.NumberExtensions.NumberToKinematicViscosity
{
    /// <summary>
    /// A number to KinematicViscosity Extensions
    /// </summary>
    public static class NumberToKinematicViscosityExtensions
    {
        /// <inheritdoc cref="KinematicViscosity.FromCentistokes(UnitsNet.QuantityValue)" />
        public static KinematicViscosity Centistokes<T>(this T value) =>
            KinematicViscosity.FromCentistokes(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromDecistokes(UnitsNet.QuantityValue)" />
        public static KinematicViscosity Decistokes<T>(this T value) =>
            KinematicViscosity.FromDecistokes(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromKilostokes(UnitsNet.QuantityValue)" />
        public static KinematicViscosity Kilostokes<T>(this T value) =>
            KinematicViscosity.FromKilostokes(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromMicrostokes(UnitsNet.QuantityValue)" />
        public static KinematicViscosity Microstokes<T>(this T value) =>
            KinematicViscosity.FromMicrostokes(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromMillistokes(UnitsNet.QuantityValue)" />
        public static KinematicViscosity Millistokes<T>(this T value) =>
            KinematicViscosity.FromMillistokes(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromNanostokes(UnitsNet.QuantityValue)" />
        public static KinematicViscosity Nanostokes<T>(this T value) =>
            KinematicViscosity.FromNanostokes(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromSquareFeetPerSecond(UnitsNet.QuantityValue)" />
        public static KinematicViscosity SquareFeetPerSecond<T>(this T value) =>
            KinematicViscosity.FromSquareFeetPerSecond(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromSquareMetersPerSecond(UnitsNet.QuantityValue)" />
        public static KinematicViscosity SquareMetersPerSecond<T>(this T value) =>
            KinematicViscosity.FromSquareMetersPerSecond(Convert.ToDouble(value));

        /// <inheritdoc cref="KinematicViscosity.FromStokes(UnitsNet.QuantityValue)" />
        public static KinematicViscosity Stokes<T>(this T value) =>
            KinematicViscosity.FromStokes(Convert.ToDouble(value));

    }
}