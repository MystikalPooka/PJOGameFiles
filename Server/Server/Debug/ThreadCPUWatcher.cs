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
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Server.Debug
{
    class ThreadCPUWatcher
    {
        [DllImport("kernel32.dll")]
        private static extern long GetThreadTimes
            (IntPtr threadHandle, out long createionTime,
             out long exitTime, out long kernelTime, out long userTime);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();

        bool watcherRunning = false;
        long percentage = -1;
        long cpuTimeStart;
        long cpuTimeEnd;

        public ThreadCPUWatcher() {
        }

        public ThreadCPUWatcher(Int16 nativeThreadID) {
        }

        public bool IsRunning {
            get { return watcherRunning; }
        }

        public long CPUusage {
            get { return percentage; }
        }

        public void StopWatcher(long elapsedMilliseconds) {
            watcherRunning = false;

            cpuTimeEnd = GetThreadTimes();

            long cpuDiff = (cpuTimeEnd - cpuTimeStart) / 10000;
            if (elapsedMilliseconds > 0) {
                percentage = (long)((cpuDiff / elapsedMilliseconds) * 100);
            } else {
                percentage = 0;
            }

            if (percentage > 100) percentage = 100;

            Thread.EndThreadAffinity(); 
        }

        public void Start() {
            System.Threading.Thread.BeginThreadAffinity();

            watcherRunning = true;

            cpuTimeStart = GetThreadTimes();
        }

        private long GetThreadTimes() {
            IntPtr threadHandle = GetCurrentThread();

            long notIntersting;
            long kernelTime, userTime;

            long retcode = GetThreadTimes
                (threadHandle, out notIntersting,
                out notIntersting, out kernelTime, out userTime);

            bool success = Convert.ToBoolean(retcode);
            if (!success)
                throw new Exception(string.Format
                ("failed to get timestamp. error code: {0}",
                retcode));

            long result = kernelTime + userTime;
            return result;
        }

    }
}
