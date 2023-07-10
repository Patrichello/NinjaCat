using UnityEngine;
using NUnit.Framework;

public class MovingPlatformsTests
{
    [Test]
    public void MovingPlatforms_Start_AtStartingPoint()
    {
        // Arrange
        var movingPlatforms = new GameObject().AddComponent<MovingPlatforms>();
        movingPlatforms.points = new Transform[2];
        movingPlatforms.points[0] = new GameObject().transform;
        movingPlatforms.points[1] = new GameObject().transform;
        var startingPoint = 0;
        var expectedPosition = movingPlatforms.points[startingPoint].position;

        // Act
        movingPlatforms.Start();

        // Assert
        Assert.AreEqual(expectedPosition, movingPlatforms.transform.position);
    }
}
