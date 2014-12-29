## Welcome
Welcome to NPlant.  NPlant is intended to be a FluentAPI in C# for creating UML diagrams.  The "N" is 
for .NET and the "Plant" is for PlantUML.  PlantUML is a textual DSL for UML.  It is extremely powerful and 
very useful on it it's own.  NPlant augments PlantUML by bringing the artifact authoring experience 
to code and automates the process of generating the PlantUML notation and subsequently the final 
artifact (PNG, JPEG, etc.).  You can find more information on PlantUML here:  http://plantuml.sourceforge.net/

## Why NPlant?
UML artifacts can be very useful, but if they live disconnected from the software system's source code, they can 
easily rot or be a burden to maintain.  As the software system changes, in order for the artifacts to remain 
useful, they too much change in step.  Modern refactoring tools such as JetBrain's Resharper make it very easy 
to change the source code (i.e. class renames, etc), but anything disconnected from the source code 
will not be acted upon by these tools.  Bringing the artifacts into the code codifies it as a first class thing 
that must be maintained, makes them easier to discover and evolve with the software system.

This library is not intended to replace PlantUML - it's simply a tool to help create PlantUML notation.  There are 
plenty of scenarios where authoring PlantUML notation by hand still makes great sense.  If you're looking to create 
artifacts for existing software systems build in .NET, or you are ready to fully automate the artifact creation 
process with your CI build tools and processes, NPlant can help you tremendously.

## How does it work?
Much like NHibernate or AutoMapper where you do declare meta models in code, NPlant offers C# classes that represent your 
atifacts like so:

```csharp
public class SimpleHasManyDiagram : ClassDiagram
{
    public SimpleHasManyDiagram()
    {
        this.AddClass<Person>();
    }
}

public class Person
{
    public IList<Hand> Hands { get; set; }
}

public class Hand
{
            
}
```

If you run that through our our nant task like so:

```xml
<?xml version="1.0"?>
<project name="test" default="main" basedir=".">
	<target name="main">
		<nplant assembly="bin\Debug\NPlant.Tests.dll" dir="generation" />
	</target>
</project>
```
It will produce a file in the "generation" folder named "SimpleHasManyDiagram.nplant".  That file will contain PlantUML
notation for a class diagram like so:

```
@startuml
class "Person" {
}
"Person" --o "Hand" : "has many\nHand"
class "Hand" {
}
@enduml
```

Which can be fed to the PlantUML commandline tool to produce an image like so:


![SimpleHasManyDiagram.PNG](http://www.nplant.org/images/SimpleHasManyDiagram.png)

## What's the current state?
I just started this project based on needs I had in my day job.  This is very much in alpha phase and I expect major 
API changes to occur for some time.  Please take a dependency on this library with the idea that things are going to be 
changing rapidly.  Today it only support Class Diagrams and generated them automatically in a rather opinionated way.  My 
goal was to create intelligent defaults that errored on the side of verbosity, but provide means to suppress verbosity 
as needed.

## What platforms does it support?
At present it's support .NET 4.5.  I haven't tested it with anything else, but have plans to support a much broader 
array of platforms and framework versions when I'm in a position to be able to effectively tests all of them on a 
repeated basis.

## What's the license?
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
 
     http://www.apache.org/licenses/LICENSE-2.0
 
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

I'm only interested in protecting myself and the effort I've put into this personally.  I want to have this as 
widely available as possible.  If this license isn't acceptable for you and your usage, let me know - I would like to 
try to accommodate you if I can.