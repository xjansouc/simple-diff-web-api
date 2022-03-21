
Console.WriteLine("Running Diff.TestClient...");

var tester = new Diff.TestClient.TestApiDiff();
await tester.Run();

Console.ReadLine();


