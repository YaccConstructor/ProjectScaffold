#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open System
open System.IO
open System.Collections.Generic

let isEdu =
    let args = System.Environment.GetEnvironmentVariable("edu_args")
    printfn "Args = %A" args
    args |> String.IsNullOrEmpty |> not
    && (args.Split ' ') |> Array.exists (fun s -> s.ToLowerInvariant() = "edu")

printfn "isEDU = %A" isEdu

let combine p1 p2 = Path.Combine(p2, p1)
let move p1 p2 =
  if File.Exists p1 then
    printfn "moving %s to %s" p1 p2
    File.Move(p1, p2)
  elif Directory.Exists p1 then
    printfn "moving directory %s to %s" p1 p2
    Directory.Move(p1, p2)
  else
    failwithf "Could not move %s to %s" p1 p2
let localFile f = combine f __SOURCE_DIRECTORY__

if isEdu
then 
  File.Delete(localFile "build.cmd")
  File.Move(localFile "build_edu.cmd", localFile "build.cmd")
  DeleteDir ".git"
  File.Delete(localFile ".gitignore")
  File.Delete(localFile ".gitattributes")
  //File.Delete("LICENSE.txt")
  //File.Delete("README.md")  

if not isEdu 
then
  File.Delete(localFile "build_edu.cmd")

File.Delete(localFile "cleanup.fsx")
printfn "finish!!!!!!!!!!!!!!!!!!!!!!!"
