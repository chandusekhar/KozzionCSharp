package com.kozzion.library.core.datastructure.financial;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.Currency;

public class EuroAmount extends ACurrencyAmount
{
    private static final long serialVersionUID = 1L;

    public EuroAmount()
    {
        this("0.00");
    }

    public EuroAmount(final String amount)
    {
        this(new BigDecimal(amount));
    }

    public EuroAmount(final BigDecimal amount)
    {
        super(Currency.getInstance("EUR"), amount);
        if (amount.scale() != d_currency.getDefaultFractionDigits())
        {
            throw new RuntimeException(amount.toString());
        }
    }

    public EuroAmount(final int cent_count)
    {
        this(create_big_decimal(cent_count));

    }

    private static BigDecimal create_big_decimal(final int cent_count)
    {
        final BigDecimal euro_count = new BigDecimal(Integer.toString(cent_count)).divide(new BigDecimal("100.00"));
        return euro_count.setScale(2);
    }

    public EuroAmount add(final EuroAmount to_add)
    {
        return new EuroAmount(d_amount.add(to_add.d_amount));
    }

    @Override
    public String toString()
    {
        return d_currency.toString() + " " + d_amount.toString();
    }

    public EuroAmount multiply(final Integer value)
    {
        return new EuroAmount(d_amount.multiply(new BigDecimal(value)));
    }

    public boolean is_postive()
    {
        return (d_amount.compareTo(BigDecimal.ZERO) == 1);
    }

    public EuroAmount negative()
    {
        return multiply(-1);
    }

    public EuroAmount subtract(final EuroAmount to_suptract)
    {
        return add(to_suptract.negative());
    }

    public EuroAmount divide(final BigDecimal to_dived)
    {
        return new EuroAmount(d_amount.divide(to_dived));
    }

    public EuroAmount divide_float(final BigDecimal to_dived)
    {
        return new EuroAmount(d_amount.divide(to_dived, 2, RoundingMode.HALF_UP));
    }
}
