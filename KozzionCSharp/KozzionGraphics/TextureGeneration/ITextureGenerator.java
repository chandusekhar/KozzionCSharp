package com.kozzion.library.graphics.texturegeneration;

import com.kozzion.library.graphics.image.CyclicFloatRgbRasterImage2D;

public interface ITextureGenerator
{
    public CyclicFloatRgbRasterImage2D generate_texture(CyclicFloatRgbRasterImage2D input_image, int height, int width);

}
