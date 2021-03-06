// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.MixedReality.Toolkit.Utilities.Gltf.Schema
{
    /// <summary>
    /// Interpolation algorithm.
    /// https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/animation.sampler.schema.json
    /// </summary>
    public enum GltfInterpolationType
    {
        LINEAR,
        STEP,
        CATMULLROMSPLINE,
        CUBICSPLINE
    }
}