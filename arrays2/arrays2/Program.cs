Console.Write("Lenght of first array: ");
int firstArrayLenght = int.Parse(Console.ReadLine());
Console.Write("Lenght of second array: ");
int secondArrayLenght = int.Parse(Console.ReadLine());

if (firstArrayLenght != secondArrayLenght)
{
    Console.WriteLine("The arrays are not equal!");
    return;
}

int[] firstArray = new int[firstArrayLenght];
int[] secondArray = new int[secondArrayLenght];


for (int i = 0; i < firstArrayLenght; i++)
{
    Console.Write($"Index {i} in the first array: ");
    firstArray[i] = int.Parse(Console.ReadLine());

}
for (int i = 0; i < firstArrayLenght; i++)
{
    Console.Write($"Index {i} in the second array: ");
    secondArray[i] = int.Parse(Console.ReadLine());
}

for (int i = 0; i < firstArrayLenght; i++)
{
    if(firstArray[i] != secondArray[i])
    {
        Console.WriteLine("The arrays are not equal!");
        return;
    }
}

Console.WriteLine("The arrays are equal!");
