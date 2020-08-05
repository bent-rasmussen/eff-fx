using Nessos.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Eff_Fx.FileSystemIO
{
	public static class FileSystemTest
    {
		public static async Task Test(string root)
		{
			// Physical file system interpretation of file system effects
			var physicalHandler = new PhysicalFileSystemHandler(root);

			// Run test
			await TestImpl().Run(physicalHandler);
		}

		static async Eff TestImpl()
		{
			// collect file infos
			var results = new List<dynamic>();

			// write a new hello world file
			var testPath = @$"temp\hello-world-{Guid.NewGuid()}.txt";
			using var stream = await FileEffect.OpenWrite(testPath);
			using var writer = new StreamWriter(stream);
			await writer.WriteLineAsync($"Hello world! {DateTimeOffset.Now}");
			await Task.Delay(200);

			// gets all files in the "temp" folder
			foreach (var info in await DirectoryIO.Enumerate("temp"))
			{
				var result =
					new
					{
						Path = info.path,
						Kind = info.kind,
						PhysicalPath = await FileSystemEffect.ToPhysicalPath(info),
						Exists = await FileSystemEffect.Exists(info),
						Created = await FileSystemEffect.CreationTime(info),
						Changed = await FileSystemEffect.LastWriteTime(info),
						Length = (info.kind == FileKind.File ? (long?)await FileEffect.Length(info.path) : null),
					};
				Console.WriteLine(result.ToString());
				results.Add(result);
			}

			// TODO print
		}
	}
}
