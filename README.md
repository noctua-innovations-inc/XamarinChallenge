<h1>Xamarin 48 Hour Challenge</h1>

<h2>Problem Definition:</h2>
<p>Demonstrate proficiency in Android software development using Xamarin by completing a 48 hour coding challenge.  The test coding challenge must be a C# project created with Xamarin.Android, (NOT Xamarin.Forms) using MVVM pattern preferably.</p>

<h2>Solution:</h2>
<p>This project was the test coding challenge's output, with end-user data validation being performed with Data Annotations</p>
<ul>
<li><a href="https://dev.azure.com/noctua-innovations/_git/Xamarin%2048%20Hour%20Challenge?path=/Android/CodeX.Android/Resources/layout" target="_blank">Views are defined as Android layout resources.</a></li>
<li><a href="https://dev.azure.com/noctua-innovations/_git/Xamarin%2048%20Hour%20Challenge?path=/Android/CodeX.Android/Activities">View implementations (a.k.a. Android Activities) do not use Xamarin.Forms.</a></li>
<li><a href="https://dev.azure.com/noctua-innovations/_git/Xamarin%2048%20Hour%20Challenge?path=/Common/CodeX.Core/Models/CzSqliteDbContext.cs">Data persistence uses Entity Framework Core (EF Core) with SQLite.</a></li>
<li><a href="https://dev.azure.com/noctua-innovations/_git/Xamarin%2048%20Hour%20Challenge?path=/Common/CodeX.Core/Models/CzAccount.cs">Data Validation uses C# Data Annotations.</a></li>
</ul>


<h2>Glossary</h2>

<h3>Microsoft Xamarin</h3>
<p>Xamarin is a platform for developing cross-platform mobile applications. It allows developers to use C# and the .NET framework to build mobile applications that can run on various operating systems, including iOS and Android. Xamarin uses a single codebase to target multiple platforms, which can save time and resources compared to developing separate native applications for each platform.</p>

<h3>C# Attributes</h3>
<p>In C#, attributes are metadata annotations that provide additional information about code elements, such as classes, methods, properties, and parameters. Attributes are used to add descriptive and declarative information to the elements they are applied to. They do not affect the runtime behavior of the code but are used by tools, frameworks, and libraries to influence their behavior or to extract information about the code.</p>

<h3>C# Data Annotations</h3>
<p>Microsoft Data Annotations, often referred to as "Data Annotations," is a set of <b>C# Attributes</b> that are part of the System.ComponentModel.DataAnnotations namespace in the .NET framework. These attributes provide a declarative way to add metadata and validation rules to classes and properties in C# or VB.NET. They are commonly used for data validation, specifying formatting rules, and other data-related tasks. Data Annotations are frequently employed in various parts of .NET, including ASP.NET MVC, Entity Framework, and more.</p>
<h4>C# Data Annotations</h4>
<ul>
<li>Are a <b>Cross-Cutting Concern</b>, because they are not specific to a particular data object element.</li>
<li>Have the <b>Single Responsibility</b> of associating metadata to code elements.</li>
<li>Facilitate the <b>Open-Closed Principle</b> by extending functionality without directly modifying the implementation.</li>
<li>Are well established C# Attributes that first became generally available on April 12, 2010.</li>
</ul>

<h3>Data Transfer Object (DTO) & Entity Framework Plain Old C# Object (POCO)</h3>
<p>A Data Transfer Object (DTO) is a design pattern used in software development to transfer data between different parts of a program or between different software systems. DTOs are simple, plain data structures that typically represent a subset of data from a more complex domain model or database entity. They are primarily used for efficiently passing data between layers of an application, such as between a client and a server, or between different components of a software system.</p>
<p>An Entity Framework (EF) Plain Old C# Object (POCO) is a DTO that is used to transfer data between the persistent data storage the software processes</p>

<h3>Cross-Cutting Concerns</h3>
<p>Cross-cutting concerns, in the context of software development, refer to aspects of a program or system that affect multiple parts of the application but are not localized to a specific module or component. These concerns "cut across" various parts of the codebase, often in a way that makes them difficult to modularize within individual components. They are typically orthogonal to the core functionality of the software and can include aspects such as security, logging, error handling, and performance optimization.</p>

<h3>Single Responsibility Principle (SRP)</h3>
<p>The Single Responsibility Principle (SRP) is one of the five SOLID principles of object-oriented programming and design. It is a fundamental concept in software engineering that emphasizes that a class or module should have only one reason to change. In other words, a class or module should have a single responsibility or a single well-defined task.</p>

<h3>Open-Closed Principle (OCP)</h3>
<p>"Software entities (such as classes, modules, functions, etc.) should be open for extension but closed for modification."</p>