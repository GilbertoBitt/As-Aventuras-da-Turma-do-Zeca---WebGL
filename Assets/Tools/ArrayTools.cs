public static class ArrayTools {

    public static T[] RemoveAt<T>(this T[] oArray, int idx) {
        T[] nArray = new T[oArray.Length - 1];
        for (int i = 0; i < nArray.Length; ++i) {
            nArray[i] = (i < idx) ? oArray[i] : oArray[i + 1];
        }
        return nArray;
    }

}
