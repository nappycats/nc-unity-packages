using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>Renderer/MaterialPropertyBlock helpers.</summary>
    public static partial class NcTweenExtensions
    {
        static MaterialPropertyBlock s_Block;
        static MaterialPropertyBlock Block => s_Block ?? (s_Block = new MaterialPropertyBlock());

        public static TweenHandle NcShaderFloatTo(this Renderer renderer, int propertyId, float to, float duration)
        {
            return NcTween.To(() =>
            {
                renderer.GetPropertyBlock(Block);
                return Block.GetFloat(propertyId);
            }, v =>
            {
                renderer.GetPropertyBlock(Block);
                Block.SetFloat(propertyId, v);
                renderer.SetPropertyBlock(Block);
            }, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcShaderFloatTo(this Renderer renderer, int propertyId, float to, in NcTweenOptions options)
        {
            return NcTween.To(() =>
            {
                renderer.GetPropertyBlock(Block);
                return Block.GetFloat(propertyId);
            }, v =>
            {
                renderer.GetPropertyBlock(Block);
                Block.SetFloat(propertyId, v);
                renderer.SetPropertyBlock(Block);
            }, to, options);
        }

        public static TweenHandle NcShaderFloatTo(this Renderer renderer, string propertyName, float to, float duration)
        {
            return NcShaderFloatTo(renderer, Shader.PropertyToID(propertyName), to, duration);
        }

        public static TweenHandle NcShaderFloatTo(this Renderer renderer, string propertyName, float to, in NcTweenOptions options)
        {
            return NcShaderFloatTo(renderer, Shader.PropertyToID(propertyName), to, options);
        }

        public static TweenHandle NcShaderColorTo(this Renderer renderer, int propertyId, Color to, float duration)
        {
            return NcTween.To(() =>
            {
                renderer.GetPropertyBlock(Block);
                return Block.GetColor(propertyId);
            }, v =>
            {
                renderer.GetPropertyBlock(Block);
                Block.SetColor(propertyId, v);
                renderer.SetPropertyBlock(Block);
            }, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcShaderColorTo(this Renderer renderer, int propertyId, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() =>
            {
                renderer.GetPropertyBlock(Block);
                return Block.GetColor(propertyId);
            }, v =>
            {
                renderer.GetPropertyBlock(Block);
                Block.SetColor(propertyId, v);
                renderer.SetPropertyBlock(Block);
            }, to, options);
        }

        public static TweenHandle NcShaderColorTo(this Renderer renderer, string propertyName, Color to, float duration)
        {
            return NcShaderColorTo(renderer, Shader.PropertyToID(propertyName), to, duration);
        }

        public static TweenHandle NcShaderColorTo(this Renderer renderer, string propertyName, Color to, in NcTweenOptions options)
        {
            return NcShaderColorTo(renderer, Shader.PropertyToID(propertyName), to, options);
        }

        public static TweenHandle NcShaderVectorTo(this Renderer renderer, int propertyId, Vector4 to, float duration)
        {
            return NcTween.To(() =>
            {
                renderer.GetPropertyBlock(Block);
                return Block.GetVector(propertyId);
            }, v =>
            {
                renderer.GetPropertyBlock(Block);
                Block.SetVector(propertyId, v);
                renderer.SetPropertyBlock(Block);
            }, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcShaderVectorTo(this Renderer renderer, int propertyId, Vector4 to, in NcTweenOptions options)
        {
            return NcTween.To(() =>
            {
                renderer.GetPropertyBlock(Block);
                return Block.GetVector(propertyId);
            }, v =>
            {
                renderer.GetPropertyBlock(Block);
                Block.SetVector(propertyId, v);
                renderer.SetPropertyBlock(Block);
            }, to, options);
        }

        public static TweenHandle NcShaderVectorTo(this Renderer renderer, string propertyName, Vector4 to, float duration)
        {
            return NcShaderVectorTo(renderer, Shader.PropertyToID(propertyName), to, duration);
        }

        public static TweenHandle NcShaderVectorTo(this Renderer renderer, string propertyName, Vector4 to, in NcTweenOptions options)
        {
            return NcShaderVectorTo(renderer, Shader.PropertyToID(propertyName), to, options);
        }
    }
}
