# APPR-Part-2
📋 Table of Contents
Features

Technology Stack

Quick Start

Project Structure

Development Setup

Azure DevOps

Database Setup

Testing

Deployment

Contributing

Support

License

✨ Features
🔐 Authentication & Authorization
Secure User Registration with ASP.NET Core Identity

Role-based Access Control (Admin, Moderator, Volunteer, User)

Profile Management with personal information and preferences

Social Login Integration (Google, Microsoft)

🚨 Disaster Incident Management
Real-time Incident Reporting with geolocation support

Multi-type Disaster Support (Floods, Earthquakes, Wildfires, etc.)

Severity Level Classification (1-5 scale)

Incident Verification Workflow

Public Incident Dashboard

💝 Resource Donation System
Multiple Donation Types: Monetary, Goods, Services

Category-based Donation Management (Food, Clothing, Medical, Shelter)

Donation Tracking & Status Updates

Targeted Donation Allocation to specific incidents

Donation History & Receipts

👥 Volunteer Coordination
Volunteer Registration with skills and availability

Task Management System with assignment capabilities

Skills Matching Algorithm

Volunteer Dashboard with task tracking

Communication Tools for coordinators

🏗️ Administrative Features
Comprehensive Dashboard with analytics

User Management and role assignment

Incident Verification and status management

Donation Coordination and distribution tracking

Reporting and Analytics

🛠 Technology Stack
Backend
Framework: ASP.NET Core 8.0 MVC

Authentication: ASP.NET Core Identity

Database: Entity Framework Core with SQL Server

ORM: Entity Framework Core 8.0

Testing: xUnit, Moq, Coverlet

Frontend
UI Framework: Bootstrap 5.3

Icons: Font Awesome 6.4

JavaScript: jQuery 3.6

Styling: Custom CSS with CSS3 variables

Responsive Design: Mobile-first approach

DevOps & Infrastructure
Version Control: Azure Repos (Git)

CI/CD: Azure Pipelines

Hosting: Azure App Service

Database: Azure SQL Database

Monitoring: Azure Application Insights

🚀 Quick Start
Prerequisites
.NET 8.0 SDK

SQL Server 2019+ or Azure SQL

Visual Studio 2022 or VS Code

Git

Installation Steps
Clone the Repository

bash
git clone https://dev.azure.com/your-organization/DisasterAlleviationFoundation/_git/DisasterAlleviationFoundation
cd DisasterAlleviationFoundation
Restore Dependencies

bash
dotnet restore
Database Setup

bash
# Create and run migrations
dotnet ef database update
Run the Application

bash
dotnet run --project DisasterAlleviationFoundation.Web
Access the Application

Main Application: https://localhost:7000

Alternative: http://localhost:5000

📁 Project Structure
text
DisasterAlleviationFoundation/
├── 📂 DisasterAlleviationFoundation.Web/          # Main web application
│   ├── 📂 Controllers/                           # MVC Controllers
│   ├── 📂 Models/                                # Data models
│   ├── 📂 Views/                                 # Razor views
│   ├── 📂 ViewModels/                            # View models
│   ├── 📂 Services/                              # Business logic
│   ├── 📂 wwwroot/                               # Static files
│   └── Program.cs                               # Application entry point
├── 📂 DisasterAlleviationFoundation.Data/        # Data access layer
│   ├── 📂 Migrations/                           # Entity Framework migrations
│   └── ApplicationDbContext.cs                  # Database context
├── 📂 DisasterAlleviationFoundation.Tests/       # Unit tests
│   ├── 📂 Controllers/                          # Controller tests
│   ├── 📂 Services/                             # Service tests
│   └── 📂 Models/                               # Model tests
├── 📂 .azuredevops/                             # DevOps configurations
│   ├── azure-pipelines.yml                      # CI/CD pipeline
│   └── pull_request_template.md                 # PR template
├── 📂 Documentation/                            # Project documentation
├── .gitignore                                   # Git ignore rules
├── README.md                                    # This file
└── DisasterAlleviationFoundation.sln            # Solution file
⚙️ Development Setup
Branching Strategy (Gitflow)
text
main (protected) → Production releases
├── production (protected) → Production deployment
├── testing → Testing environment
└── develop → Development integration
    ├── feature/* → New features
    ├── bugfix/* → Bug fixes
    └── hotfix/* → Critical fixes
Development Workflow
Create Feature Branch

bash
git checkout develop
git checkout -b feature/your-feature-name
Make Changes and Commit

bash
git add .
git commit -m "feat: add your feature description"
Push and Create Pull Request

bash
git push origin feature/your-feature-name
# Create PR in Azure DevOps
Code Style Guidelines
Use PascalCase for class names and methods

Use camelCase for local variables and parameters

Follow ASP.NET Core naming conventions

Use meaningful names for variables and methods

Include XML documentation for public APIs

🔄 Azure DevOps
Build Pipeline
The application uses Azure Pipelines for continuous integration with the following stages:

Build & Test

.NET 8 SDK installation

NuGet package restoration

Solution compilation

Unit test execution

Code coverage collection

Artifact publication

Deploy to Test (develop and testing branches)

Automatic deployment to test environment

Environment-specific configuration

Deploy to Production (production branch)

Manual approval required

Production environment deployment

Pipeline Triggers
Automatic: On push to develop, testing, production branches

Pull Request: Validation on PR creation

Scheduled: Daily builds for main branch

Quality Gates
✅ Minimum 2 code reviewers required

✅ All tests must pass

✅ Code coverage threshold (80% minimum)

✅ No build warnings

✅ Successful security scan

🗄️ Database Setup
Local Development
Using LocalDB

bash
dotnet ef migrations add InitialCreate
dotnet ef database update
Connection String (appsettings.Development.json)

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DisasterAlleviationFoundation;Trusted_Connection=true"
  }
}
Production Database
Azure SQL Database recommended for production

Automatic backups configured

Performance monitoring enabled

Security: Azure Active Directory authentication

Database Schema
Key entities include:

Users (Extended IdentityUser)

IncidentReports (Disaster incidents)

Donations (Resources and funds)

Volunteers (Volunteer registrations)

VolunteerTasks (Assignment tasks)

🧪 Testing
Test Structure
bash
DisasterAlleviationFoundation.Tests/
├── UnitTests/           # Unit tests
├── IntegrationTests/    # Integration tests
└── TestData/           # Test data builders
Running Tests
bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Specific test project
dotnet test DisasterAlleviationFoundation.Tests
Test Coverage
Controllers: 85%+ coverage

Services: 90%+ coverage

Models: Validation testing

Integration: End-to-end scenarios

🚀 Deployment
Environments
Environment	Branch	URL	Purpose
Development	develop	Internal	Feature development
Testing	testing	https://test-daf.azurewebsites.net	QA testing
Staging	production	https://staging-daf.azurewebsites.net	Pre-production
Production	main	https://daf.azurewebsites.net	Live application
Deployment Process
Automatic Deployment to testing environment

Manual Approval required for production

Health Checks performed post-deployment

Rollback Strategy in place for issues

Environment Configuration
Development: LocalDB, debug logging, detailed errors

Testing: Test SQL, verbose logging, test data

Production: Azure SQL, warning logging, optimized performance

👥 Contributing
We welcome contributions from the community! Please follow these guidelines:

Reporting Issues
Check existing issues before creating new ones

Use the issue templates provided

Include detailed reproduction steps

Attach relevant logs or screenshots

Feature Requests
Describe the feature and its benefits

Include use cases and examples

Consider implementation complexity

Discuss with maintainers before implementation

Pull Request Process
Fork the repository

Create a feature branch

Follow coding standards

Write or update tests

Update documentation

Submit PR with completed template

Code Review Checklist
Code follows project standards

Tests are included and passing

Documentation is updated

Security considerations addressed

Performance impact assessed
