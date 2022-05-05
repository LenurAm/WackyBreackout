using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    #region Properties
    
    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return 10; }
    }

    /// <summary>
    /// Gets the ball move units per second
    /// </summary>
    /// <value>pball speed per second</value>
    public static float BallMoveUnitsPerSecond
    { get { return 200; } }

    /// <summary>
    /// A ball life time
    /// </summary>
    public static float BallLifeSeconds
    { get { return 10; } }

    /// <summary>
    /// Minimum time between ball spawn
    /// </summary>
    public static float MinSpawnTime
    { get { return 5; } }

    /// <summary>
    /// Maximum time between ball spawn
    /// </summary>
    public static float MaxSpawnTime
    { get { return 10; } }
    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {

    }
}
