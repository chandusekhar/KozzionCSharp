using System;
namespace KozzionMachineLearning.DataSet
{
    public enum DataLevel
    {
        BINARY,
        NOMINAL,
        ORDINAL,
        DATE_TIME, // For dates, they are common in datasets
        INTERVAL,
        UNIQUE //For IDs or complex hyrachical data structures

    }
}