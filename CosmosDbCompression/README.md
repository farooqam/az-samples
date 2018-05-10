# Azure Cosmos DB Compression Sample
This sample demonstrates compressing documents in the application prior to storing in Cosmos DB.

## Running the sample
The **GameLogUtility** project contains a .NET Core 2.0 console app that reads text files containing baseball game data. This game data is what will be stored in Cosmos DB.
A zip archive of the text files can be downloaded from [here.](http://www.retrosheet.org/gamelogs/gl2010_17.zip)
After downloading the zip, extract it to a folder. 

Build the **GameLogUtility** project. Then open a command line and navigate to the project directory. The following command executes the app:

```posh
dotnet run -- [text file folder] [Cosmos DB endpoint] [Cosmos DB auth key] [database name] compress
```

Omitting the **compress** switch will not compress the text files prior to storing in Cosmos DB.

More details are in [my blog.]()