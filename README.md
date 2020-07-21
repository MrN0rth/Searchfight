# Searchfight
*This is a project created as programming challenge.*

Used technologies: .NET Core 3.1

## Project purpose

Compare number of results returned by different search engines.

## Used search engines

1. Google [API](https://developers.google.com/custom-search)
2. Bing [API](https://docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/)

## Input

App receives several arguments separated with space.
It also supports terms with spaces escaped with quotation marks.

For example: 

```searchfight.exe .net java "java script"```

## Output

App displays general results by inputted topic, shows "winner" by each seach engines and finally, determines total winner by topic. 

![alt text](https://img.techpowerup.org/200721/screenshot-1.png)

## Before start

Before running this app, you need to configure both Google and Bing auth keys in *appsettings.json* file:

**Google**

ApiKey - key of application for custom search

CustomSearchEngineId - id of custom search engine separately configured in Custom Sezrch Engine control panel

**Bing**

ApiKey - application key obtained from previously created Azure Cognitive Services

*Note: Microsoft Azure subscription is requred to use Cognitive Services*

