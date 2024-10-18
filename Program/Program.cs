using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal class Program
{
    Executor executor;
    CodeBlock? codeEntry;
    string code = "";

    internal Program(Executor executor) 
    {
        this.executor = executor;
    }

    internal void Start()
    {
        if (codeEntry == null)
            return;
        codeEntry.Execute();

        WriteEndProgram();
    }

    void WriteEndProgram()
    {

        executor.Execute((Field f) => 
        {
            f.log += ".\r\nEnd state (" + f.guy.p.X + "," + f.guy.p.Y + ") facing ";


            switch (f.guy.direction)
            {
                case (0):
                    f.log += "east";
                    break;
                case (1):
                    f.log += "south";
                    break;
                case (2):
                    f.log += "west";
                    break;
                case (3):
                    f.log += "north";
                    break;

            }

            f.log +=".\r\n";


        });
    }


    internal void Parse(string txt)
    {
        code = txt;
        Parse();
        GetStatistics();
    }

    void Parse()
    {
        //split string on newline tokens
        char[] del = { '\n', '\r'};
        List<string> ss = new(code.Split(del).Where((string s) => s != ""));
        

        //save command and indentation in new list
        List<(int indent, string command)> cmds = new();
        foreach (string s in ss) 
        {
            List<char> cs = new(s);
            int n = 0;
            
            for (; 0 < cs.Count && cs[0] == '\t'; n++) { cs.RemoveAt(0); }

            cmds.Add((n, new(cs.ToArray())));
        }

        //parse commands to linked CodeBlocks
        int line = 0;

        codeEntry = ParseCommands(0);

        CodeBlock? ParseCommands(int indentation)
        {
            //if line doesn't exist return null to signify the end of a list of commands
            if (line >=  cmds.Count)
            {
                return null;
            }

            //the end of an indented block has been reached if the indent doesn't match return null without changing the line
            if (cmds[line].indent != indentation)
            {
                return null;
            }


            string[] cmd = cmds[line].command.Split(' ');
            line++;


            if (cmd.Length == 0)
            {
                return ParseCommands(indentation);
            }
            CodeBlock cb = CommandRecognition(cmd, indentation);

            cb.nextCommand = ParseCommands(indentation);


            return cb;


        }

        CodeBlock CommandRecognition(string[] cmd, int indentation)
        {
            CodeBlock cb;

            switch (cmd[0])
            {
                case ("Move"):
                    {
                        cb = new Move(executor, int.Parse(cmd[1]));
                    }
                    break;
                case "Turn":
                    {
                        if (cmd[1] == "right")
                        {
                            cb = new Turn(executor, 1);
                            break;
                        }
                        if (cmd[1] == "left")
                        {
                            cb = new Turn(executor, -1);
                            break;
                        }
                        throw new Exception("Direction " + cmd[1] + " not recognised. Did you mean right or left?");
                    }
                case "Repeat":
                    {
                        cb = new Repeat(executor, int.Parse(cmd[1]), ParseCommands(indentation + 1));
                    }
                    break;
                default:
                    {
                        throw new Exception("command " + cmd[0] + " not recognised");
                    }
            }
            return cb;
        }

    }

    internal string GetStatistics()
    {
        if (codeEntry == null)
            return "";
        Statistics s = codeEntry.GetStatistics();
        return "Depth = " + s.depth + "\nCommandCount = " + s.count;
    }


}
