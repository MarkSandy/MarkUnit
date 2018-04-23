# MarkUnit
Architectual Unittesting

Add architectual constraints to your solution. Enforce strict layers by checking assembly and class dependencies.

Example:


    // We load a solution by specifying the path and a name filter. We could also specify any assembly. 
    var solution = Solution.Create().FromPath("c:\myProjects").Matching("XY.*.dll"); 
    
    // We now declare our architectual rules and check them
    solution.EachClass().That().ImplementsInterface<IBackEndService>().Should().BeInAssemblyMatching("XY.Services").Check();
    solution.NoAssembly().That().Not().HasNameMatching("XY.DataController").Should().ReferenceAssembliesMatching("System.Data").Check();
    ...
    

Basically, you will first create a solution, i.e. load all assemblies of your solution. There are two ways to do this. 
The first described above will load all assemblies from a given that match a certain pattern.
The second method is done by specifying the main assembly, e.g. the assembly of your 'program'.

    var solution = Solution.Create().FromAssembly (typeof(MainWindow).Assembly).Matching("XY.*.dll");
	
This will load all assemblies that are referenced by the given assembly and their references. As this will also include all .NET and third party assemblies, you might consider filtering to use only the assemblies of your solution.

    
The project is in it's alpha state and requires tests, review and code improvements.
    

You can now create your first constraint and check it.

   solution
     .EachClass()
	 .That()
	 .ImplementsInterface<IBackEndService>()
	 .Should()
	 .BeInAssemblyMatching("XY.Services")
	 .Check();
	 
The last method to call must be 'Check()'. It will evaluate the constraint and eventually throws an exception together with the constraint and all failing elements..