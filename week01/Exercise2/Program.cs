using System;

class Program
{
    static void Main()
    {
        // Solicitar al usuario el porcentaje de calificación
        Console.Write("Ingrese su porcentaje de calificación: ");
        int percentage = int.Parse(Console.ReadLine());

        // Variable para almacenar la letra de calificación
        string letter = "";

        // Determinar la letra de calificación basada en el porcentaje
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determinar si el estudiante aprobó o no
        if (percentage >= 70)
        {
            Console.WriteLine("¡Felicidades, aprobaste el curso!");
        }
        else
        {
            Console.WriteLine("No aprobaste el curso. ¡Ánimo para la próxima vez!");
        }

        // Variable para almacenar el signo de la calificación
        string sign = "";

        // Solo agregar signo para A, B, C y D
        if (letter != "F")
        {
            int lastDigit = percentage % 10;

            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            // Si no es ni + ni -, no hacemos nada
        }

        // Si la calificación no es A+ ni F+, mostramos la letra con el signo
        if (letter == "A" && sign == "+")
        {
            letter = "A-"; // No se debe permitir A+
        }
        else if (letter == "F" && sign != "")
        {
            sign = ""; // No se permite F+ ni F-
        }

        // Imprimir la calificación final con el signo si corresponde
        Console.WriteLine("Tu calificación final es: " + letter + sign);
    }
}
