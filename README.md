# CSV Compare Algorithm

Author: IxxI5

### Description

This algorithm is designed to evaluate deviations in, e.g., vehicle driving behavior by comparing data from a reference and a measurement file (.csv). It automatically identifies unique column names within the.csv files, even when multiple lines of text precede the desired columns, as well as whether the data are formatted with comma or semicolon separators. It facilitates a robust comparison between reference and measurement data, accommodating varying lengths of measurement series, which can be equal to, larger, or smaller than the reference data. Furthermore, the algorithm efficiently processes noisy or shifted measurement data, tolerating shifts up to 10 seconds with a sampling rate of 10 milliseconds.
