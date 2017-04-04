package com.kozzion.library.core.datastructure.financial;

import java.io.Serializable;
import java.math.BigDecimal;
import java.util.Currency;

public class ACurrencyAmount implements Serializable
{
    private static final long serialVersionUID = 1L;

    protected Currency        d_currency;
    protected BigDecimal      d_amount;

    protected ACurrencyAmount(final Currency currency, final BigDecimal amount)
    {
        d_currency = currency;
        d_amount = amount;
    }

    public Currency get_currency()
    {
        return d_currency;
    }

    public BigDecimal get_amount()
    {
        return d_amount;
    }

    @Override
    public boolean equals(final Object other)
    {
        if (other instanceof ACurrencyAmount)
        {
            final ACurrencyAmount other_currency_amount = (ACurrencyAmount) other;
            if (other_currency_amount.d_currency.equals(d_currency) && other_currency_amount.d_amount.equals(d_amount))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
