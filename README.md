# Garmin Export

The program exports all activities from garmin connect in the original file format. Usually as .fit

## Prerequisites

To run the .NET Core Runtime 2.0 must be installed.

## Run

dotnet GarminExport.dll -u <<garmin login email address>> -p <<garmin login password>> 

### Command Line Parameter

Required:

-u						Garmin Connect email address
-p						Garmin Connect password

Optional:

-o 						Relative or absolute output path; Default "./export"
--from-date				Start date in format yyy-mm-dd
--save-activity-result 	Saves the server response of an individual activity in the output folder as a file <<activityId.json>>>

## License

This project is licensed under the Apache License Version 2.0 - see the [LICENSE.md](LICENSE.md) file for details