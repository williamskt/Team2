using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	public float moveTime = .1f;
	public LayerMask blockingLayer;

	private BoxCollider2D bc;
	private Rigidbody2D rb;
	private float inverseMoveTime;

	// Use this for initialization
	void Start () {
		bc = GetComponent<BoxCollider2D>;
		rb = GetComponent<Rigidbody2D>;
		inverseMoveTime = 1f / moveTime;
	}

	protected bool Move (int xDir, int yDir, out RaycastHit2D hit){
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		bc.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		bc.enabled = true;

		if (hit.transform == null) {
			StartCoroutine (SmoothMovement (end));
			return true;
		}
		return false;
	}
		
	protected IEnumerator SmoothMovement (Vector3 end){
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (rb.position, end, inverseMoveTime * Time.deltaTime);
			rb.MovePosition (newPosition);
			yield return null;
		}
	}
	
	protected virtual void AttemptMove <T> (int xDir, int yDir)
		where T : Component
	{
		RaycastHit2D hit;
		bool canMove = Move (xDir, yDir, out hit);

		if (hit.transform == null)
			return;

		T hitComponent = hit.transform.GetComponent<T> ();

		if (!canMove && hitComponent != null)
			OnCantMove (hitComponent);
	}
}
