# Couchbase, WebAPI, AngularJS, .NET Stack Example

A very basic example of a CEAN-type stack application that makes use of Couchbase Server's N1QL query language.

The full stack application separates the ASP.NET/WebAPI/Couchbase Server into the back-end and leaves AngularJS, HTML, and CSS as the front-end that requests data from the back-end and presents it to the user.

## Prerequisites

There are not many prerequisites required to build and run this project, but you'll need the following:

* Visual Studio (with NuGet)
* Couchbase Server 4+

## Installation & Configuration

Certain configuration in both the application and the database must be done before this project is usable.

### Application

Checkout the latest master branch from GitHub and open RestfulDotnet.sln in Visual Studio.

Build the solution (this should install all dependencies via NuGet).

### Database

This project requires Couchbase 4.0 or higher in order to function because it makes use of the N1QL query language.  With Couchbase Server installed, create a new bucket called **restful-sample** or whatever you've named it in your Web.config file.

In order to use N1QL queries in your application you must create a primary index on your bucket.  This can be done by using the Couchbase Query Client (CBQ).

On Windows, run the following to launch CBQ:

```
C:/Program Files/Couchbase/Server/bin/cbq.exe
```

With CBQ running, create an index like so:

```
CREATE PRIMARY INDEX ON `restful-sample` USING GSI;
```

Your database is now ready for use.

If you are using Couchbase 4.5, you can execute this query in the Query Workbench on Couchbase Console.

## Testing

Run your web application from Visual Studio (CTRL+F5, for instance).

The site should appear in a browser, something like: **http://localhost:12345**.

## Resources

Couchbase - [http://www.couchbase.com](http://www.couchbase.com)

AngularJS - [http://www.angularjs.org](http://www.angularjs.org)

ASP.NET - [http://asp.net](http://asp.net)

ASP.NET WebAPI - [http://www.asp.net/web-api](http://www.asp.net/web-api)