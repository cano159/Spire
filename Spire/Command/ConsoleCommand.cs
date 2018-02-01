using Monocle;

namespace Spire.Command
{
    /// <summary>
    /// A class that allows the addition of new console commands to the TowerFall console.
    /// </summary>
    public abstract class ConsoleCommand
    {
        public string CommandString { get; }

        /// <summary>
        /// Object constructor.
        /// </summary>
        /// <param name="commandString">The console command string.</param>
        protected ConsoleCommand(string commandString) => CommandString = commandString;

        /// <summary>
        /// The method to invoke upon command entry into the console.
        /// </summary>
        /// <param name="args">The arguments passed from the console.</param>
        /// 
        public abstract void Invoke(string[] args);
        
        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message) => Commands.Trace(message);
    }
}