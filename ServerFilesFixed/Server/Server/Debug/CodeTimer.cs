/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Server.Debug
{
    public class CodeTimer
    {
        Stopwatch stopwatch;
        Dictionary<string, TimeSpan> resultsCollection;
        string currentSectionName;
        string codeSection;

        public CodeTimer(string codeSection) {
            stopwatch = new Stopwatch();
            resultsCollection = new Dictionary<string, TimeSpan>();
            this.codeSection = codeSection; 
        }

        public void StartTimingSection(string sectionName) {
            this.currentSectionName = sectionName;
            stopwatch.Start();
        }

        public void EndTimingSection() {
            stopwatch.Stop();
            resultsCollection.Add(this.currentSectionName, stopwatch.Elapsed);
            stopwatch.Reset();
        }

        public string GetResults() {
            long totalTicks = 0;
            foreach (TimeSpan timeSpan in resultsCollection.Values) {
                totalTicks += timeSpan.Ticks;
            }
            StringBuilder resultString = new StringBuilder();
            resultString.Append("--- ");
            resultString.Append(this.codeSection);
            resultString.AppendLine(" ---");
            foreach (string sectionName in resultsCollection.Keys) {
                // Append section name
                resultString.Append("[");
                resultString.Append(sectionName);
                resultString.Append("] ");
                TimeSpan timeSpan = resultsCollection[sectionName];
                // Append time taken
                resultString.Append(timeSpan.ToString());
                // Append percentage of total time
                resultString.Append(" (");
                resultString.Append(PMU.Core.MathFunctions.CalculatePercent(timeSpan.Ticks, totalTicks));
                resultString.AppendLine("%)");
            }
            return resultString.ToString();
        }
    }
}
