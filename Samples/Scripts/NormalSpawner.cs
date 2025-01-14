/*
* NormalSpawner.cs
*
* This script is licensed under the MIT License.
* See the LICENSE file in the root of the repository for more details.
*
* Copyright (c) 2024 Srdan Jokic
*/

using Godot;
using System;

namespace EasyPool.Samples;

public partial class NormalSpawner : Node
{
    [Export] private Godot.Collections.Array<Node> _spawnedContainers;
    [Export] private PackedScene _projectile;

    private bool _isActive;

    public void Toggle(bool activate)
    {
        _isActive = activate;
    }

    public override void _Process(double delta)
    {
        if (!_isActive)
        {
            return;
        }

        foreach (var spawnedContainer in _spawnedContainers)
        {
            var projectile = _projectile.Instantiate().GetNode<Projectile>(".");
            spawnedContainer.AddChild(projectile);

            projectile.Reset();
            projectile.Fire(Vector2.Right, () => projectile.QueueFree());
        }
    }
}
