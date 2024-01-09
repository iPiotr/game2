using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int orderInLayerChange = 1; // The value to change the Order in Layer when this waypoint is touched

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") // Assuming your player has the tag "Player"
        {
            // Find the "Grid" GameObject
            GameObject gridObject = GameObject.Find("Grid");
            Debug.Log(gridObject);
            if (gridObject)
            {
                SpriteRenderer gridRenderer = gridObject.GetComponent<SpriteRenderer>();
                if (gridRenderer)
                {
                    Debug.Log("Waypoint sadsadsadasdtouchedsadsadd!");
                    gridRenderer.sortingLayerName = "Back2";
                    gridRenderer.sortingOrder += orderInLayerChange;
                }
            }
            Destroy(gameObject); // Optionally destroy the waypoint after it's touched
        }
    }
}
