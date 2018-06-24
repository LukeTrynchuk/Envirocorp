using UnityEngine;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The Command Scriptable Object is 
    /// an abstract class which defines
    /// what all commands should implement.
    /// </summary>
    public abstract class Command : ScriptableObject
    {
        public abstract string CommandString { get; }
        public abstract string CommandDefinition { get; }
        public virtual string Execute() { return ""; }
    }
}
