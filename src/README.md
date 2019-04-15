# Samil Portal

This library retrieves data from the [Samil Portal website](http://www.samilportal.com/) based on the plant name.

Example to get the values for this year:
```c#
var helper = new SamilHelper("plant five");
var values = helper.GetYearValues(DateTime.Now);
```

Example to get the values for this month:
```c#
var helper = new SamilHelper("plant five");
var values = helper.GetMonthValues(DateTime.Now);
```

Example to get the values for today:
```c#
var helper = new SamilHelper("plant five");
var values = helper.GetDayValues(DateTime.Now);
```