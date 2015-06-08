set opencover_exe="..\packages\OpenCover.4.5.3723\OpenCover.Console.exe"
set reportgenerator_exe="..\packages\ReportGenerator.2.1.4.0\ReportGenerator.exe"
set test_exe="..\packages\xunit.runner.console.2.0.0\tools\xunit.console.exe"

%opencover_exe% -output:coverage.xml -target:%test_exe% -targetargs:"%1 -noshadow" -filter:"+[*]* -[xunit*]* -[*Tests]* -[*Interface*]*"
%reportgenerator_exe% .\coverage.xml .\coverage_results
start coverage_results\index.htm