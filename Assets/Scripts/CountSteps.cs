using UnityEngine;
using UnityEngine.UI;
using PedometerU;

public class CountSteps : MonoBehaviour
{
    private Pedometer pedometer;
    public Text stepText, distanceText, updateCount;

    public int stepCount {get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // Create a new pedometer
        pedometer = new Pedometer(OnStep);
        // Reset UI
        OnStep(0, 0);
    }

    private void OnStep(int steps, double distance)
    {
        // Display the values // Distance in feet
        this.stepCount = steps;
        stepText.text = steps.ToString();
        distanceText.text = (distance * 3.28084).ToString("F2") + " ft";

    }
    private void OnDisable()
    {
        // Release the pedometer
        pedometer.Dispose();
        pedometer = null;
    }

    // Update is called once per frame
    void Update()
    {
        updateCount.text = pedometer.updateCount.ToString();
    }
}
