# Intro to Azure Development
## Requirements 
1. [Visual Studio 2022](https://visualstudio.microsoft.com/) IDE and Code Editor for Software Developers and Teams
2. [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli)
3. GitHub Repo for tutorial https://github.com/jpl2nd/DemoWebApp7292/
4. .NET 8
5. An active Azure Subscriptions

##Instructions
You can clone the full repo and take a look at the code or follow along with the insturctions and build the repo yourself. 

### Creating the project. 
1. Open Visuals Studio
    1. Create a New Project
    2. Select ASP.NETCore Web App ( Razor Page )
    3. Name the project something relevant and save it. I will name it "wainstructions"
    4. Start the project to make sure you have all the resources you need.
    5. You should see something like this: ![image](https://github.com/user-attachments/assets/0c413e49-d6ad-4a7e-b728-6320ce963ca5)
    6. tip: you can select which browser and even which profile starts when you start you project by selecting "Browse With" under the context menu for Start. ![image](https://github.com/user-attachments/assets/6763b555-716b-493c-9364-9ec1318ffda0)
    7. If you have renamed your profile in Edge settings, you will still need to reference the profile with "Profile #", as that's how edge stores the user data.
    8. Check in the code to an new GitHub Repo under your GitHub account. 

### Create a Resource Group
1. Open the Azure Portal [https://portal.azure.com](https://portal.azure.com)
2. Create a Resoure Group to contain the web app.
  3. Click on Create a Resource from the Azure Home Page and search for resource group.
 
 ![image](https://github.com/user-attachments/assets/93b7d877-7f7c-4bb6-a800-b4035ab026dc) 

  5. Click Create 
  6. Name the Resource Group

### Create a Web App
1. Navigate to the new resource group
2. Click Create and search for web app
3.   ![image](https://github.com/user-attachments/assets/19704606-74f6-440f-bea4-ebd5e2c01c60)
4.   Name the web app
5.   Select .NET 8 LTS for the runtime stack
6.   Click Next: Deployment
7.   Click "Enable"
8.   Log in to your GitHub Account.
9.   Select the organization, Repo and Brancg
10.   Click on Next: network, then Next Monitor
11.   Enable Application Insights
12.   Click Create New to create a new instance of Application Insights
13.   Click Next to enter tags
15.   Click Review and Create, Then Create

### Github Action
1. Navigate to your GitHub repo and click on "Actions"
2. You should see your the newly created Action building and deploying your site; but it will likely error out.
3. Click on the workflow run with the Red X next to it and you will see this error:
4. ![image](https://github.com/user-attachments/assets/4285f111-5ddc-4bc8-ae54-a8bb841588a6)
5. Navigate back to the Code view of your repo and click into '.github/Workflows'
6. Open the "master_[your web app name].yml" file
7. Search for "myapp"
8. You should see something like

          - name: dotnet publish
                run: dotnet publish -c Release -o ${{env.DOTNET_ROOT}}/myapp
    
              - name: Upload artifact for deployment job
                uses: actions/upload-artifact@v4
                with:
                  name: .net-app
                  path: ${{env.DOTNET_ROOT}}/myapp

9. Chante "myapp" to the name of the project you created in step 1.3 "wainstructions" for me.
10. Commit the changes and ensure your project builds and deploys to your new website.

### Implement Appllication insights




  
