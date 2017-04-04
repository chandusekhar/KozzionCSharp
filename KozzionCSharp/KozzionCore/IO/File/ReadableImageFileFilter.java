package com.kozzion.library.core.file.filter;

import javax.imageio.ImageIO;

public class ReadableImageFileFilter extends ExtentionFileFilter
{
    public ReadableImageFileFilter()
    {
        super();
        final String [] extensions = ImageIO.getReaderFileSuffixes();
        for (final String extension : extensions)
        {
            addExtension(extension);
        }
        setDescription("All posible image types");
    }
}
