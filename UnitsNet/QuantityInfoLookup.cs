﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace UnitsNet
{
    /// <summary>
    /// A collection of <see cref="QuantityInfo"/>.
    /// </summary>
    /// <remarks>
    ///     Access type is <c>internal</c> until this class is matured and ready for external use.
    /// </remarks>
    internal class QuantityInfoLookup
    {
        private readonly Lazy<QuantityInfo[]> _infosLazy;
        private readonly Lazy<Dictionary<(Type, string), UnitInfo>> _unitTypeAndNameToUnitInfoLazy;

        /// <summary>
        /// New instance.
        /// </summary>
        /// <param name="quantityInfos"></param>
        public QuantityInfoLookup(ICollection<QuantityInfo> quantityInfos)
        {
            Names = quantityInfos.Select(qt => qt.Name).ToArray();

            _infosLazy = new Lazy<QuantityInfo[]>(() => quantityInfos
                .OrderBy(quantityInfo => quantityInfo.Name)
                .ToArray());

            _unitTypeAndNameToUnitInfoLazy = new Lazy<Dictionary<(Type, string), UnitInfo>>(() =>
            {
                return Infos
                    .SelectMany(quantityInfo => quantityInfo.UnitInfos
                        .Select(unitInfo => new KeyValuePair<(Type, string), UnitInfo>(
                            (unitInfo.Value.GetType(), unitInfo.Name),
                            unitInfo)))
                    .ToDictionary(x => x.Key, x => x.Value);
            });
        }

        /// <summary>
        /// All enum value names of <see cref="Infos"/>, such as "Length" and "Mass".
        /// </summary>
        public string[] Names { get; }

        /// <summary>
        /// All quantity information objects, such as <see cref="Length.Info"/> and <see cref="Mass.Info"/>.
        /// </summary>
        public QuantityInfo[] Infos => _infosLazy.Value;

        /// <summary>
        /// Get <see cref="UnitInfo"/> for a given unit enum value.
        /// </summary>
        public UnitInfo GetUnitInfo(Enum unitEnum) => _unitTypeAndNameToUnitInfoLazy.Value[(unitEnum.GetType(), unitEnum.ToString())];

        /// <summary>
        /// Try to get <see cref="UnitInfo"/> for a given unit enum value.
        /// </summary>
        public bool TryGetUnitInfo(Enum unitEnum, [NotNullWhen(true)] out UnitInfo? unitInfo) =>
            _unitTypeAndNameToUnitInfoLazy.Value.TryGetValue((unitEnum.GetType(), unitEnum.ToString()), out unitInfo);

        /// <summary>
        ///
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="unitInfo"></param>
        public void AddUnitInfo(Enum unit, UnitInfo unitInfo)
        {
            _unitTypeAndNameToUnitInfoLazy.Value.Add((unit.GetType(), unit.ToString()), unitInfo);
        }

        /// <summary>
        ///     Dynamically construct a quantity.
        /// </summary>
        /// <param name="value">Numeric value.</param>
        /// <param name="unit">Unit enum value.</param>
        /// <returns>An <see cref="IQuantity"/> object.</returns>
        /// <exception cref="ArgumentException">Unit value is not a know unit enum type.</exception>
        public IQuantity From(QuantityValue value, Enum unit)
        {
            // TODO Support custom units, currently only hardcoded built-in quantities are supported.
            if (Quantity.TryFrom(value, unit, out IQuantity? quantity))
                return quantity;

            throw new ArgumentException(
                $"Unit value {unit} of type {unit.GetType()} is not a known unit enum type. Expected types like UnitsNet.Units.LengthUnit. Did you pass in a third-party enum type defined outside UnitsNet library?");
        }

        /// <inheritdoc cref="Quantity.TryFrom(QuantityValue,System.Enum,out UnitsNet.IQuantity)"/>
        public bool TryFrom(double value, Enum unit, [NotNullWhen(true)] out IQuantity? quantity)
        {
            // Implicit cast to QuantityValue would prevent TryFrom from being called,
            // so we need to explicitly check this here for double arguments.
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                quantity = default(IQuantity);
                return false;
            }

            // TODO Support custom units, currently only hardcoded built-in quantities are supported.
            return Quantity.TryFrom((QuantityValue)value, unit, out quantity);
        }

        /// <inheritdoc cref="Parse(IFormatProvider, System.Type,string)"/>
        public IQuantity Parse(Type quantityType, string quantityString) => Parse(null, quantityType, quantityString);

        /// <summary>
        ///     Dynamically parse a quantity string representation.
        /// </summary>
        /// <param name="formatProvider">The format provider to use for lookup. Defaults to <see cref="CultureInfo.CurrentCulture" /> if null.</param>
        /// <param name="quantityType">Type of quantity, such as <see cref="Length"/>.</param>
        /// <param name="quantityString">Quantity string representation, such as "1.5 kg". Must be compatible with given quantity type.</param>
        /// <returns>The parsed quantity.</returns>
        /// <exception cref="ArgumentException">Type must be of type UnitsNet.IQuantity -or- Type is not a known quantity type.</exception>
        public IQuantity Parse(IFormatProvider? formatProvider, Type quantityType, string quantityString)
        {
            if (!typeof(IQuantity).IsAssignableFrom(quantityType))
                throw new ArgumentException($"Type {quantityType} must be of type UnitsNet.IQuantity.");

            // TODO Support custom units, currently only hardcoded built-in quantities are supported.
            if (Quantity.TryParse(formatProvider, quantityType, quantityString, out IQuantity? quantity))
                return quantity;

            throw new ArgumentException($"Quantity string could not be parsed to quantity {quantityType}.");
        }

        /// <inheritdoc cref="Quantity.TryParse(IFormatProvider,System.Type,string,out UnitsNet.IQuantity)"/>
        public bool TryParse(Type quantityType, string quantityString, [NotNullWhen(true)] out IQuantity? quantity)
        {
            // TODO Support custom units, currently only hardcoded built-in quantities are supported.
            return Quantity.TryParse(null, quantityType, quantityString, out quantity);
        }

        /// <summary>
        ///     Get a list of quantities that has the given base dimensions.
        /// </summary>
        /// <param name="baseDimensions">The base dimensions to match.</param>
        public IEnumerable<QuantityInfo> GetQuantitiesWithBaseDimensions(BaseDimensions baseDimensions)
        {
            return _infosLazy.Value.Where(info => info.BaseDimensions.Equals(baseDimensions));
        }
    }
}