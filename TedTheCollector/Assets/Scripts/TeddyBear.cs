using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collecting teddy bear
/// </summary>
public class TeddyBear : MonoBehaviour
{
	#region Fields

	const float ImpulseForceMagnitude = 2.0f;

	bool collecting = false;
	GameObject targetPickup;

	// saved for efficiency
	// we needed to add new before the rigidbody2D field
	// for reasons that will become clear when we cover
	// inheritance
	new Rigidbody2D rigidbody2D;
	TedTheCollector tedTheCollector;

	#endregion

	#region Methods

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
    {
		// center teddy bear in screen
		transform.position = Vector3.zero;

		// save references for efficiency
		rigidbody2D = GetComponent<Rigidbody2D>();
		tedTheCollector = Camera.main.GetComponent<TedTheCollector>();
	}

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button
    /// over the collider
    /// </summary>
    void OnMouseDown()
    {
		// ignore mouse clicks if already collecting
		if (!collecting)
        {
			GoToNextPickup();
		}
	}

    /// <summary>
    /// Called when another object is within a trigger collider
    /// attached to this object
    /// </summary>
    /// <param name="other">collider info</param>
    void OnTriggerStay2D(Collider2D other)
    {
		// only respond if the collision is with the target pickup
		if (other.gameObject == targetPickup)
        {
			// remove collected pickup from game and go to the next one
			tedTheCollector.RemovePickup(targetPickup);
			rigidbody2D.velocity = Vector2.zero;
			GoToNextPickup();
		}
	}

	/// <summary>
	/// Starts the teddy bear moving toward the next pickup
	/// </summary>
	void GoToNextPickup()
    {
		// calculate direction to target pickup and start moving toward it
		targetPickup = tedTheCollector.TargetPickup;
		if (targetPickup != null)
		{
			Vector2 direction = new Vector2(
				targetPickup.transform.position.x - transform.position.x,
				targetPickup.transform.position.y - transform.position.y);
			direction.Normalize();
			rigidbody2D.AddForce(direction * ImpulseForceMagnitude, 
				ForceMode2D.Impulse);
			collecting = true;
		}
		else
		{
			collecting = false;
		}
	}

	#endregion
}
