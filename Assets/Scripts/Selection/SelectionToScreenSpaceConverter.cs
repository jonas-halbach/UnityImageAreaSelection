using UnityEngine;

public class SelectionToScreenSpaceConverter {

    private const int rectCornerCount = 4;
    private RectTransform Selection {
        get;set;
    }

    private Vector3[] LocalSelectionCorners {
        get; set;
    }

    private Vector3 UpperLeftSelectionCorner {
        get {
            return LocalSelectionCorners[(int)SelectionRectCorners.UPPER_LEFT];
        }
    }

    private Vector3 LowerRightSelectionCorner {
        get {
            return LocalSelectionCorners[(int)SelectionRectCorners.LOWER_RIGHT];
        }
    }

    public Vector3 UpperLeftCornerScreenSpace {
        get; private set;
    }

    public Vector3 LowerRightCornerScreenSpace {
        get; private set;
    }

    public float WidthScreenSpace {
        get; private set;
    }

    public float HeightScreenSpace {
        get; private set;
    }

    private Camera Camera {
        get;set;
    }

    public SelectionToScreenSpaceConverter(RectTransform selection, Camera camera) {
        Selection = selection;
        Camera = camera;

        Initialize();
    }

    private void Initialize() {
        LocalSelectionCorners = GetRectConrnersInWorldSpace();

        TransformSelectionRectCornersToScreenSpace();
        TransformDimensionsToScreenSpace();
    }

    private Vector3[] GetRectConrnersInWorldSpace() {
        Vector3[] localSelectionCorners = new Vector3[rectCornerCount];
        Selection.GetWorldCorners(localSelectionCorners);

        return localSelectionCorners;
    }

    private void TransformSelectionRectCornersToScreenSpace() {
        UpperLeftCornerScreenSpace = TransformToScreenSpace(UpperLeftSelectionCorner);
        LowerRightCornerScreenSpace = TransformToScreenSpace(LowerRightSelectionCorner);
    }

    private Vector4 TransformToScreenSpace(Vector3 point)
    {
        Vector4 pointInWorldSpace = new Vector4(point.x, point.y, 0, 1);
        return TransformToScreenSpace(pointInWorldSpace);
    }

    private Vector4 TransformToScreenSpace(Vector4 point)
    {
        return Camera.WorldToScreenPoint(point);
    }

    private void TransformDimensionsToScreenSpace() {
        Vector3 diagonal = CalculateLowerRightUpperLeftDiagonal();
        WidthScreenSpace = diagonal.x;
        HeightScreenSpace = diagonal.y;
    }

    private Vector3  CalculateLowerRightUpperLeftDiagonal() {
        return LowerRightCornerScreenSpace - UpperLeftCornerScreenSpace;
    }
}

public enum SelectionRectCorners {
    UPPER_LEFT = 0,
    LOWER_RIGHT = 2,
}
