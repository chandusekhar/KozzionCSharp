package com.kozzion.library.graphics.texturegeneration;

public class TexGen
{
    private static int R = 0;
    private static int G = 1;
    private static int B = 2;

    // An three channel index function
    private int a(final int x, final int y, final int w)
    {
        return 3 * ((y * w) + x);
    }

    // An two channal index function
    private int aa(final int x, final int y)
    {
        return 2 * ((y * data.d_width_out) + x);
    }

    int              vrstartx;
    int              vrfinishx;
    int              vrstarty;
    int              vrfinishy;
    int []           candlistx;
    int []           candlisty;

    int []           atlas;

    boolean          anotherpass;
    int              maxcand;

    int []           xloopout;
    int []           yloopout;
    // int[] xloopin;
    // int[] yloopin;

    TexGenParameters data;

    float []         target;

    public TexGen(final TexGenParameters parameters)
    {
        data = parameters;
        anotherpass = false;
        maxcand = 40;
    }

    // /////////////////////////////////////////////////////////////////
    // This is the main texture synthesis function. Called just once
    // from main to generate image from 'image' into 'result'.
    // Synthesis parameters (image and neighborhood sizes) are in global
    // 'data' structure.
    // /////////////////////////////////////////////////////////////////

    public void create_texture(final float [] image, final float [] target, final float [] result)
    {
        int i, j, k, ncand;
        int bestx = -1;
        int besty = -1;
        double diff, curdiff;
        this.target = target;

        // read_ppm("14.t.ppm",tsx,tsy,target);
        // if(tsx != data.widthout || tsx != data.heightout){
        // printf("Target image is of incorrect size. Exiting\n");
        // exit(1);
        // }
        // Atlas is to hold information about where in the input image did a
        // pixel came from. In principle, only need a small atlas, but
        // updating it would complicate the code, so make it of the size
        // of the output image.

        // atlas = (int *)malloc(2*data.widthout*data.heightout*sizeof(int));
        // atlasy = (int *)malloc(data.widthout*data.heightout*sizeof(int));

        final int [] candlistx = new int [(data.d_local_x * data.d_local_y) + 1];
        final int [] candlisty = new int [(data.d_local_x * data.d_local_y) + 1];

        final boolean anotherpass = false;

        if (!anotherpass)
        {
            init(result, image);
        }
        System.out.println("init finished");

        // /////////////////////////////////////////////////////////////////////
        // This is the start of the main texture synthesis loop. If there is
        // anything worth optimizing, it's here (and, obviously, in functions
        // it calls): initialization ('init' function)
        // and edge handling (a loop below this one) consume currently just a
        // fraction of total time.
        // ////////////////////////////////////////////////////////////////////

        for (i = 0; i < (data.d_height_out - (data.d_local_y / 2)); i++)
        {
            for (j = 0; j < data.d_width_out; j++)
            {
                // First, create a list of candidates for particular pixel.
                if (anotherpass)
                {
                    ncand = create_all_candidates(j, i);
                }
                else
                {
                    ncand = create_candidates(j, i);
                }

                // If there are multiple candidates, choose the best based on L_2
                // norm

                if (ncand > 1)
                {
                    diff = 1e10;
                    for (k = 0; k < ncand; k++)
                    {
                        curdiff = compare_neighb(image, candlistx[k], candlisty[k], result, j, i);
                        curdiff += compare_rest(image, candlistx[k], candlisty[k], target, j, i);
                        if (curdiff < diff)
                        {
                            diff = curdiff;
                            bestx = candlistx[k];
                            besty = candlisty[k];
                        }
                    }
                }
                else
                {
                    bestx = candlistx[0];
                    besty = candlisty[0];
                }

                // Copy the best candidate to the output image and record its position
                // in the atlas (atlas is used to create candidates)

                // bestx=besty = 0;
                result[a(j, i, data.d_width_out) + R] = image[a(bestx, besty, data.d_width_in) + R];
                result[a(j, i, data.d_width_out) + G] = image[a(bestx, besty, data.d_width_in) + G];
                result[a(j, i, data.d_width_out) + B] = image[a(bestx, besty, data.d_width_in) + B];
                atlas[aa(j, i)] = bestx;
                atlas[aa(j, i) + 1] = besty;
            }
        }

        // Use full neighborhoods for the last few rows. This is a small
        // fraction of total area - can be ignored for optimization purposes.

        for (; i < data.d_height_out; i++)
        {
            for (j = 0; j < data.d_width_out; j++)
            {
                ncand = create_all_candidates(j, i);
                // ncand = create_candidates(j,i);
                if (ncand > 1)
                {
                    diff = 1e10;
                    for (k = 0; k < ncand; k++)
                    {
                        curdiff = compare_full_neighb(image, candlistx[k], candlisty[k], result, j, i);
                        if (curdiff < diff)
                        {
                            diff = curdiff;
                            bestx = candlistx[k];
                            besty = candlisty[k];
                        }
                    } // for k
                }
                else
                {
                    bestx = candlistx[0];
                    besty = candlisty[0];
                }
                result[a(j, i, data.d_width_out) + R] = image[a(bestx, besty, data.d_width_in) + R];
                result[a(j, i, data.d_width_out) + G] = image[a(bestx, besty, data.d_width_in) + G];
                result[a(j, i, data.d_width_out) + B] = image[a(bestx, besty, data.d_width_in) + B];
                atlas[aa(j, i)] = bestx;
                atlas[aa(j, i) + 1] = besty;
            }
        }
        System.out.println("Finished first pass\n");

        // /////////////////////////////////////////////////////////////////////
        // End of main texture synthesis loop
        // /////////////////////////////////////////////////////////////////////

        // Redo first couple of rows for better vertical tilability
        // This takes a small fraction of total time, so it can be ignored for now

        // for(i=0;i<data.heightout;i++){
        for (i = 0; i < (data.d_local_y / 2); i++)
        {
            for (j = 0; j < data.d_width_out; j++)
            {
                ncand = create_all_candidates(j, i);
                if (ncand > 1)
                {
                    diff = 1e10;
                    for (k = 0; k < ncand; k++)
                    {
                        curdiff = compare_full_neighb(image, candlistx[k], candlisty[k], result, j, i);
                        if (curdiff < diff)
                        {
                            diff = curdiff;
                            bestx = candlistx[k];
                            besty = candlisty[k];
                        }
                    }
                }
                else
                {
                    bestx = candlistx[0];
                    besty = candlisty[0];
                }
                result[a(j, i, data.d_width_out) + R] = image[a(bestx, besty, data.d_width_in) + R];
                result[a(j, i, data.d_width_out) + G] = image[a(bestx, besty, data.d_width_in) + G];
                result[a(j, i, data.d_width_out) + B] = image[a(bestx, besty, data.d_width_in) + B];
                atlas[aa(j, i)] = bestx;
                atlas[aa(j, i) + 1] = besty;
            }
        }
        // postprocess(result);
        System.out.println("Finished frame\n");
        // write_ppm(data.widthout, data.heightout, "zad.ppm",result);
        // printf("Finished wrtiting frame\n");
        // free(atlas);
    }

    // Marks boundaries of texture pieces. Not necessary - ignore it now.

    void postprocess(final float [] result)
    {
        int i, j, sx, sy;
        for (i = 1; i < (data.d_height_out - 1); i++)
        {
            for (j = 1; j < (data.d_width_out - 1); j++)
            {
                sx = atlas[aa(j, i)];
                sy = atlas[aa(j, i) + 1];
                if (!((sx == (atlas[aa(j + 1, i)] - 1)) && (sx == (atlas[aa(j - 1, i)] + 1)) && (sy == (atlas[aa(j, i + 1) + 1] - 1)) && (sy == (atlas[aa(
                    j, i - 1) + 1] + 1))))
                {
                    result[a(j, i, data.d_width_out) + R] = 1.0f;
                    result[a(j, i, data.d_width_out) + G] = 1.0f;
                    result[a(j, i, data.d_width_out) + B] = 1.0f;
                    /*
                     * result[a(j,i,data.widthout)+R] = (result[a(j-1,i,data.widthout)+R]+
                     * result[a(j+1,i,data.widthout)+R]+result[a(j,i-1,data.widthout)+R]+
                     * result[a(j,i+1,data.widthout)+R]+result[a(j-1,i-1,data.widthout)+R]+
                     * result[a(j-1,i+1,data.widthout)+R]+result[a(j+1,i-1,data.widthout)+R]+
                     * result[a(j+1,i+1,data.widthout)+R])/8; result[a(j,i,data.widthout)+G] =
                     * (result[a(j-1,i,data.widthout)+G]+
                     * result[a(j+1,i,data.widthout)+G]+result[a(j,i-1,data.widthout)+G]+
                     * result[a(j,i+1,data.widthout)+G]+result[a(j-1,i-1,data.widthout)+G]+
                     * result[a(j-1,i+1,data.widthout)+G]+result[a(j+1,i-1,data.widthout)+G]+
                     * result[a(j+1,i+1,data.widthout)+G])/8; result[a(j,i,data.widthout)+B] =
                     * (result[a(j-1,i,data.widthout)+B]+
                     * result[a(j+1,i,data.widthout)+B]+result[a(j,i-1,data.widthout)+B]+
                     * result[a(j,i+1,data.widthout)+B]+result[a(j-1,i-1,data.widthout)+B]+
                     * result[a(j-1,i+1,data.widthout)+B]+result[a(j+1,i-1,data.widthout)+B]+
                     * result[a(j+1,i+1,data.widthout)+B])/8;
                     */
                }
            }
        }
    }

    // Creates a list of valid candidates for given pixel using only L-shaped
    // causal area

    int create_candidates(final int x, final int y)
    {
        int address, i, j, k, n = 0;
        for (i = 0; i <= (data.d_local_y / 2); i++)
        {
            for (j = -data.d_local_x / 2; j <= (data.d_local_x / 2); j++)
            {
                if ((i == 0) && (j >= 0))
                {
                    continue;
                }
                address = aa((data.d_width_out + x + j) % data.d_width_out, ((data.d_width_out + y) - i) % data.d_width_out);
                // address = aa(xloopout[x + j], yloopout[y - i]);
                candlistx[n] = atlas[address] - j;
                candlisty[n] = atlas[address + 1] + i;

                if ((candlistx[n] >= vrfinishx) || (candlistx[n] < vrstartx))
                {
                    candlistx[n] = vrstartx + (int) (drand48() * (vrfinishx - vrstartx));
                    candlisty[n] = vrstarty + (int) (drand48() * (vrfinishy - vrstarty));
                    n++;
                    continue;
                }

                // double tmp = drand48(); tmp *= tmp; tmp *=tmp;
                if (candlisty[n] >= vrfinishy)
                {
                    // || tmp > ((double)vrfinishy-candlisty[n])/(vrfinishy-vrstarty) ){
                    candlisty[n] = vrstarty + (int) (drand48() * (vrfinishy - vrstarty));
                    candlistx[n] = vrstartx + (int) (drand48() * (vrfinishx - vrstartx));
                    n++;
                    continue;
                }
                for (k = 0; k < n; k++)
                {
                    if ((candlistx[n] == candlistx[k]) && (candlisty[n] == candlisty[k]))
                    {
                        n--;
                        break;
                    }
                }
                n++;
                // if(n == maxcand) return n;
            }
        }
        /*
         * for(i=-data.localy/2;i<0;i++){ for(j=-data.localx/2;j<=data.localx/2;j++){ address =
         * a((data.widthout+x+j)%data.widthout, (data.heightout+y-i)%data.heightout,data.widthout); if(target[address+B]
         * != 1.0){ address = aa((data.widthout+x+j)%data.widthout, (data.heightout+y-i)%data.heightout); candlistx[n] =
         * atlas[address]; candlisty[n] = atlas[address+1]; n++; return n; } } }
         */
        return n;
    }

    // Created a list of candidates using the complete square around the pixel

    int create_all_candidates(int x, int y)
    {
        // HACK
        if (x < 3)
        {
            x = 3;
        }
        if (y < 3)
        {
            y = 3;
            // end HACK
        }

        int address, i, j, k, n = 0;
        for (i = -data.d_local_y / 2; i <= (data.d_local_y / 2); i++)
        {
            for (j = -data.d_local_x / 2; j <= (data.d_local_x / 2); j++)
            {
                if ((i == 0) && (j >= 0))
                {
                    continue;
                }
                // address = aa((data.widthout+x+j)%data.widthout,
                // (data.heightout+y-i)%data.heightout);
                address = aa(xloopout[x + j], yloopout[y - i]);
                candlistx[n] = atlas[address] - j;
                candlisty[n] = atlas[address + 1] + i;

                if ((candlistx[n] >= vrfinishx) || (candlistx[n] < vrstartx))
                {
                    candlistx[n] = vrstartx + (int) (drand48() * (vrfinishx - vrstartx));
                    candlisty[n] = vrstarty + (int) (drand48() * (vrfinishy - vrstarty));
                    n++;
                    continue;
                }

                // tmp = drand48(); tmp *= tmp; tmp *=tmp;
                if ((candlisty[n] >= vrfinishy) || (candlisty[n] < vrstarty))
                {
                    // || tmp > ((double)vrfinishy-candlisty[n])/(vrfinishy-vrstarty) ){
                    candlisty[n] = vrstarty + (int) (drand48() * (vrfinishy - vrstarty));
                    candlistx[n] = vrstartx + (int) (drand48() * (vrfinishx - vrstartx));
                    n++;
                    continue;
                }
                for (k = 0; k < n; k++)
                {
                    if ((candlistx[n] == candlistx[k]) && (candlisty[n] == candlisty[k]))
                    {
                        n--;
                        break;
                    }
                }
                n++;
                // if(n == maxcand) return n;
            }
        }
        return n;
    }

    // Initializes the output image and atlases to a random collection of pixels

    private double drand48()
    {
        return Math.random();
    }

    private void init(final float [] result, final float [] image)
    {
        int i, j, tmpx, tmpy;
        xloopout = new int [data.d_width_out * data.d_height_out];
        yloopout = new int [data.d_width_out * data.d_height_out];
        atlas = new int [data.d_width_out * data.d_height_out * 4];
        candlistx = new int [data.d_width_out * data.d_height_out * 4];
        candlisty = new int [data.d_width_out * data.d_height_out * 4];

        vrstartx = data.d_local_x / 2;
        vrstarty = data.d_local_y / 2;
        vrfinishx = data.d_width_in - (data.d_local_x / 2);
        vrfinishy = data.d_height_in - (data.d_local_y / 2);
        for (i = 0; i < data.d_height_out; i++)
        {
            for (j = 0; j < data.d_width_out; j++)
            {
                if ((target[a(j, i, data.d_width_out) + R] == 1.0) && (target[a(j, i, data.d_width_out) + G] == 1.0)
                    && (target[a(j, i, data.d_width_out) + B] == 1.0))
                {
                    tmpx = vrstartx + (int) (drand48() * (vrfinishx - vrstartx));
                    tmpy = vrstarty + (int) (drand48() * (vrfinishy - vrstarty));
                    if (!anotherpass)
                    {
                        atlas[aa(j, i)] = tmpx;
                        atlas[aa(j, i) + 1] = tmpy;
                        result[a(j, i, data.d_width_out) + R] = image[a(tmpx, tmpy, data.d_width_in) + R];
                        result[a(j, i, data.d_width_out) + G] = image[a(tmpx, tmpy, data.d_width_in) + G];
                        result[a(j, i, data.d_width_out) + B] = image[a(tmpx, tmpy, data.d_width_in) + B];
                    }
                }
            }
        }
        return;
    }

    // Compares two square neighborhoods, returns L_2 difference

    double compare_full_neighb(final float [] image, int x, int y, final float [] image1, int x1, int y1)
    {
        // HACK
        if (x < 3)
        {
            x = 3;
        }
        if (y < 3)
        {
            y = 3;
        }
        if (x1 < 3)
        {
            x1 = 3;
        }
        if (y1 < 3)
        {
            y1 = 3;
            // end HACK
        }

        double tmp, res = 0;
        int i, j, addr, addr1;
        for (i = -(data.d_local_y / 2); i <= (data.d_local_y / 2); i++)
        {
            for (j = -(data.d_local_x / 2); j <= (data.d_local_x / 2); j++)
            {
                if (!((i > 0) && (y1 > data.d_local_y) && ((y1 + i) < data.d_height_out)))
                {
                    addr = a(x + j, y + i, data.d_width_in);
                    // addr1 = a((data.widthout+x1+j)%data.widthout,
                    // (data.heightout+y1+i)%data.heightout,data.widthout);
                    addr1 = a(xloopout[x1 + j], yloopout[y1 + i], data.d_width_out);

                    tmp = image[addr + R] - image1[addr1 + R];
                    res += tmp * tmp;
                    tmp = image[addr + G] - image1[addr1 + G];
                    res += tmp * tmp;
                    tmp = image[addr + B] - image1[addr1 + B];
                    res += tmp * tmp;
                }
            }
        }
        return res;
    }

    // Compares two L-shaped neighborhoods, returns L_2 difference

    double compare_neighb(final float [] image, int x, int y, final float [] image1, int x1, int y1)
    {
        // HACK
        if (x < 3)
        {
            x = 3;
        }
        if (y < 3)
        {
            y = 3;
        }
        if (x1 < 3)
        {
            x1 = 3;
        }
        if (y1 < 3)
        {
            y1 = 3;
            // end HACK
        }

        double tmp, res = 0;
        int i, j, addr, addr1;
        for (i = -(data.d_local_y / 2); i < 0; i++)
        {
            for (j = -(data.d_local_x / 2); j <= (data.d_local_x / 2); j++)
            {
                addr = a(x + j, y + i, data.d_width_in);
                addr1 = a((data.d_width_out + x1 + j) % data.d_width_out, (data.d_height_out + y1 + i) % data.d_height_out,
                    data.d_width_out);
                // addr1 = a(xloopout[x1 + j], yloopout[y1 + i], data.d_width_out);

                tmp = image[addr + R] - image1[addr1 + R];
                res += tmp * tmp;
                tmp = image[addr + G] - image1[addr1 + G];
                res += tmp * tmp;
                tmp = image[addr + B] - image1[addr1 + B];
                res += tmp * tmp;
            }
        }

        // return res;
        for (j = -(data.d_local_x / 2); j < 0; j++)
        {
            // for(j=-(data.localx/2);j<=data.localx/2;j++){

            addr = a(x + j, y, data.d_width_in);
            // addr1 = a((data.widthout+x1+j)%data.widthout, y1,data.widthout);
            addr1 = a(xloopout[x1 + j], y1, data.d_width_out);

            tmp = image[addr + R] - image1[addr1 + R];
            res += tmp * tmp;
            tmp = image[addr + G] - image1[addr1 + G];
            res += tmp * tmp;
            tmp = image[addr + B] - image1[addr1 + B];
            res += tmp * tmp;

        }

        return res;
    }

    double compare_rest(final float [] image, int x, int y, final float [] target, int x1, int y1)
    {
        // HACK
        if (x < 3)
        {
            x = 3;
        }
        if (y < 3)
        {
            y = 3;
        }
        if (x1 < 3)
        {
            x1 = 3;
        }
        if (y1 < 3)
        {
            y1 = 3;
            // end HACK
        }

        double tmp, res = 0;
        int i, j, addr, addr1;
        for (i = (data.d_local_y / 2); i > 0; i--)
        {
            for (j = -(data.d_local_x / 2); j <= (data.d_local_x / 2); j++)
            {
                addr = a(x + j, y + i, data.d_width_in);
                // addr1 = a((data.widthout+x1+j)%data.widthout,
                // (data.heightout+y1+i)%data.heightout,data.widthout);
                addr1 = a(xloopout[x1 + j], yloopout[y1 + i], data.d_width_out);
                if (target[addr1 + B] != 1.0)
                {
                    tmp = image[addr + R] - target[addr1 + R];
                    res += tmp * tmp;
                    tmp = image[addr + G] - target[addr1 + G];
                    res += tmp * tmp;
                    tmp = image[addr + B] - target[addr1 + B];
                    res += tmp * tmp;
                }
            }
        }

        // return res;
        for (j = (data.d_local_x / 2); j > 0; j--)
        {
            // for(j=-(data.localx/2);j<=data.localx/2;j++){

            addr = a(x + j, y, data.d_width_in);
            // addr1 = a((data.widthout+x1+j)%data.widthout, y1,data.widthout);
            addr1 = a(xloopout[x1 + j], y1, data.d_width_out);
            if (target[addr1 + B] != 1.0)
            {
                tmp = image[addr + R] - target[addr1 + R];
                res += tmp * tmp;
                tmp = image[addr + G] - target[addr1 + G];
                res += tmp * tmp;
                tmp = image[addr + B] - target[addr1 + B];
                res += tmp * tmp;
            }
        }

        return res;
    }

}
