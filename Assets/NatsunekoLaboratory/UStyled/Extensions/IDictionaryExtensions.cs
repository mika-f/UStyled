// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace NatsunekoLaboratory.UStyled.Extensions
{
    // ReSharper disable once InconsistentNaming
    internal static class IDictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> obj, Dictionary<TKey, TValue> another)
        {
            foreach (var value in another)
            {
                if (obj.ContainsKey(value.Key))
                    continue;

                obj.Add(value.Key, value.Value);
            }
        }
    }
}