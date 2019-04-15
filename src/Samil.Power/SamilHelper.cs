using System;
using System.Net;
using Newtonsoft.Json;

namespace Samil.Power
{
    public class SamilHelper
    {
        public string StationName { get; }

        public SamilHelper(string stationName)
        {
            StationName = stationName;
        }

        /// <summary>
        /// Retrieving all year values from Samil Power website.
        /// </summary>
        /// <param name="date">Date to get the year from.</param>
        /// <returns>Object with dictionary that contains totals per month for the specified year. Values are in kWh.</returns>
        public SamilResult GetYearValues(DateTime date)
        {
            return GetYearValues(date.Year);
        }

        /// <summary>
        /// Retrieving all year values from Samil Power website.
        /// </summary>
        /// <param name="year">Year to get the data for.</param>
        /// <returns>Object with dictionary that contains totals per month for the specified year. Values are in kWh.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public SamilResult GetYearValues(int year)
        {
            if (year <= 0) throw new ArgumentOutOfRangeException(nameof(year));
            if (string.IsNullOrEmpty(StationName)) throw new ArgumentNullException(nameof(StationName));

            var content = DoRequest($"station-manage-year.action?model.resportYear={year}&model.chartType=1&model.initYear=true&model.stationname={StationName}");
            var result = JsonConvert.DeserializeObject<SamilResult>(content);
            return result;
        }

        
        /// <summary>
        /// Retrieving all month values from Samil Power website.
        /// </summary>
        /// <param name="date">Date to get the year and month from.</param>
        /// <returns>Object with dictionary that contains totals per day for the specified month. Values are in kWh.</returns>
        public SamilResult GetMonthValues(DateTime date)
        {
            return GetMonthValues(date.Year, date.Month);
        }

        /// <summary>
        /// Retrieving all month values from Samil Power website.
        /// </summary>
        /// <param name="year">Year to get the data for.</param>
        /// <param name="month">Month to get the data for.</param>
        /// <returns>Object with dictionary that contains totals per day for the specified month. Values are in kWh.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public SamilResult GetMonthValues(int year, int month)
        {
            if (year <= 0) throw new ArgumentOutOfRangeException(nameof(year));
            if (month <= 0 || month > 12) throw new ArgumentOutOfRangeException(nameof(month));
            if (string.IsNullOrEmpty(StationName)) throw new ArgumentNullException(nameof(StationName));

            var content = DoRequest($"station-manage-month.action?model.resportYear={year}&model.resportMonth={month}&model.chartType=1&model.initMonth=true&model.stationname={StationName}");
            var result = JsonConvert.DeserializeObject<SamilResult>(content);
            return result;
        }

        /// <summary>
        /// Retrieving all day values from Samil Power website.
        /// </summary>
        /// <param name="date">Date to get the year, month and dat from.</param>
        /// <returns>Object with dictionary that contains totals per quarter for the specified day. Values are in watts.</returns>
        public SamilDayResult GetDayValues(DateTime date)
        {
            return GetDayValues(date.Year, date.Month, date.Day);
        }

        /// <summary>
        /// Retrieving all day values from Samil Power website.
        /// </summary>
        /// <param name="year">Year to get the data for.</param>
        /// <param name="month">Month to get the data for.</param>
        /// <param name="day">Day to get the data for.</param>
        /// <returns>Object with dictionary that contains totals per quarter for the specified day. Values are in watts.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public SamilDayResult GetDayValues(int year, int month, int day)
        {
            if (year <= 0) throw new ArgumentOutOfRangeException(nameof(year));
            if (month <= 0 || month > 12) throw new ArgumentOutOfRangeException(nameof(month));
            if (day <= 0 || day > 31) throw new ArgumentOutOfRangeException(nameof(day));
            if (string.IsNullOrEmpty(StationName)) throw new ArgumentNullException(nameof(StationName));

            var content = DoRequest($"station-manage-day.action?model.stationname={StationName}&model.resportYear={year}&model.resportMonth={month}&model.resportDay={day}&model.initDaily=true");
            var result = JsonConvert.DeserializeObject<SamilDayResult>(content);
            return result;
        }

        private string DoRequest(string url)
        {
            using (var webClient = new WebClient())
            {
                webClient.BaseAddress = "http://www.samilportal.com/stationmanage/";
                var downloadString = webClient.DownloadString(url);
                return downloadString;
            }
        }
    }
}