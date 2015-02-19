using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Timers;
using System.IO;
using System;
using Microsoft.VisualStudio.Text.Editor;


namespace ReadabilityMetricTool
{
    public class ReadabilityMetricTool
    {
        private IWpfTextView _view;
        private static Timer _timer;
        private static int TotalLines = 0;
        private static int[] LineCounters;
        private static int ElapsedSeconds = 0;

        public ReadabilityMetricTool(IWpfTextView view)
        {
            _view = view;
            Initialise();
        }

        private void Initialise()
        {
            TotalLines = _view.TextSnapshot.LineCount;
            LineCounters = new int[TotalLines];
            _timer = new Timer(1000); //1000ms
            _timer.Enabled = false;
            _timer.Elapsed += new ElapsedEventHandler(UpdateLineCounters);
        }

        public void UpdateLineCounters(object sender, EventArgs e)
        {
            ElapsedSeconds++;

            int FirstVisLine = _view.TextSnapshot.GetLineNumberFromPosition(_view.TextViewLines.FirstVisibleLine.Start);
            int LastVisLine = _view.TextSnapshot.GetLineNumberFromPosition(_view.TextViewLines.LastVisibleLine.Start);

            for (int i = FirstVisLine; i <= LastVisLine; i++)
                LineCounters[i]++;

        }

        public static void ToggleTimer()
        {
            if (_timer.Enabled == false)
            {//Timer is not running
                Debug.WriteLine("Timer Starting");
                _timer.Start();
            } 
            else
            { 
                Debug.WriteLine("Timer Stopping");
                _timer.Stop();
            }
        }

        public static void WriteToTextfile()
        {
            Debug.WriteLine("Writing to textfile");
            using (StreamWriter writer = new StreamWriter("output.csv"))
            {
                writer.WriteLine("time in seconds = " + ElapsedSeconds);
                writer.WriteLine("line number, frequency");
                for (int i = 0; i < TotalLines; i++)
                {
                    writer.WriteLine((i + 1) + "," + LineCounters[i]);
                }
                writer.Close();
            }
        }
    }
}
