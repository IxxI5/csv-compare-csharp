# CSV Compare Algorithm

Author: IxxI5

### Description

This algorithm is designed to evaluate deviations in, e.g., vehicle driving behavior by comparing data from a reference and a measurement file (.csv). It automatically identifies unique column names within the.csv files, even when multiple lines of text precede the desired columns, as well as whether the data are formatted with comma or semicolon separators. It facilitates a robust comparison between reference and measurement data, accommodating varying lengths of measurement series, which can be equal to, larger, or smaller than the reference data. Furthermore, the algorithm efficiently processes noisy or shifted measurement data, tolerating shifts up to 10 seconds with a sampling rate of 10 milliseconds.

## License

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

Copyright (c) 2015 Chris Kibble

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
