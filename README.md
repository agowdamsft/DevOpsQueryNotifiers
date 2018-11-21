# DevOps Query Notifier
A Logic Apps way to get email or other ways to get notified when a new workitem lands in your query set. This project is a Azure ARM template with Logic Apps that has a time based trigger of Monday morning 8 AM every week. 

## Background
This workflow will query the list of items that need to be processed from a SharePoint List that has list of subscribers and the query to be looked it when it runs every week. Logic Apps uses DevOps REST API queries of Wiql using Basic Auth but can be changed to use different auth mechanism. There are no moving parts other than Logic Apps piece to this solution

## Features
1. Connects to a SharePoint list to check what all notifications need to sent and whom to be sent to (SharePoint Columns: Title, DevOps Query URL, Subscribers Emails, Query ID Calculated Column [=LEFT(RIGHT([Dev Ops Query],LEN([Dev Ops Query])-FIND("y/",[Dev Ops Query],1)),37)] to gran the GUID of the query
1. Posts to Microsoft Teams Channel as part of the notifications
1. Uses variables to set what all fields need to be retrieved from DevOps Query
1. Issues 2 REST API POSTS per query item
1. Subscribers have an ability to subscribe or unsubscribe to the alerts
1. Uses personal basic auth on SharePoint and Email connectors

## Open Items
1. System.Description file is not being used in the email body for now because of complexity of dealing with HTML formatted field. Use of Logic Apps expressions can help take care of this. Limiting to first 200 chars on description can keep the email to small size.

## Ignore
##HelperFunctions - This was planned to be used for comma separating the values returned by the first query but Logic Apps ForEach loop to an array was able to get this done easily.

## Deployment
This is a ARM template with connectors parameterized. Right click on VS 2017 Solution and Deploy to Azure. After successful deployment you will have open this Logic Apps in Azure Portal designer to edit and re authenticate on the Office connectors. Those items will be marked in Yellow warning sign in the portal.


Thanks to @nzthiago and @brandonh-msft for thier contributions.
