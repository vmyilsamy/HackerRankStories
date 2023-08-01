# HackerRank Best N Stories

### Pre-requisites
I have used .net 6.0 to create the web api and used nunit 3 to run unit tests. 

### Compile 
Please make sure you have installed all the relevany dependencies and code compiles without any issues

### Run
Please set BestNStories as default project and hit F5, which will launch a browser and load the api documentation; this interactive swagger UI can be used to test the api as well.

### Considerations
-- To address the specification about "not overloading of the Hacker News API", I have used caching mechanism which efficiently caches both best story ids as well as individual stories for a day and the cache expires after a day.

-- I've used parallel async code to load bunch of stories rather than one by one to be efficient, we can scale vertically by increasing the max degree of parallelism(limited to system resource).

-- Unit testing I've followed BDD style unit testing which is quite efficient to manage in the long run, when things are complicated this style of unit test will definitely help

As you can below the unit test output clearly states what the intention of the test. This helps in the long run.

![BDD style nunit tests](https://github.com/vmyilsamy/HackerRankStories/assets/9333379/b71be3f3-4e1f-48d0-8bdf-d8f6952ec4d9)


### Given more time, things I would like to do...

- Add resilience by using Polly
- Add security (authentication and authorisation)
- Include loggine middleware using Serilog
- Add more unit tests for service and components
- Add integration tests for hacker rank client class
- Add acceptance tests to cover the business acceptance criteria
