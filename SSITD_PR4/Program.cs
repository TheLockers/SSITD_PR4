using System;
using System.Collections.Generic;

namespace SSITD_PR4
{
    class Program
    {
        static void Main(string[] args)
        {
            // План проекта хранит все стадии.
            PlanProject plan = new PlanProject();

            // Стадия 1: техническое задание.
            StageProject firstStage = new StageProject(
                1,
                "Техническое задание",
                new HashSet<string>
                {
                    "Техническое задание"
                },
                new HashSet<string>
                {
                    "Техническое задание"
                });

            // Стадия 2: эскизный проект.
            StageProject secondStage = new StageProject(
                2,
                "Эскизный проект",
                new HashSet<string>
                {
                    "Пояснительная записка эскизного проекта",
                    "Ведомость эскизного проекта"
                },
                new HashSet<string>
                {
                    "Пояснительная записка эскизного проекта"
                });

            // Стадия 3: технический проект.
            StageProject thirdStage = new StageProject(
                3,
                "Технический проект",
                new HashSet<string>
                {
                    "Пояснительная записка технического проекта",
                    "План разработки и внедрения",
                    "Описание ролей и прав доступа"
                },
                new HashSet<string>
                {
                    "Пояснительная записка технического проекта",
                    "Описание ролей и прав доступа"
                });

            // Стадия 4: рабочий проект.
            StageProject fourthStage = new StageProject(
                4,
                "Рабочий проект",
                new HashSet<string>
                {
                    "Спецификация",
                    "Текст программы",
                    "Описание программы",
                    "Программа и методика испытаний",
                    "Протокол испытаний",
                    "Руководство оператора",
                    "Руководство преподавателя",
                    "Руководство студента"
                },
                new HashSet<string>
                {
                    "Спецификация",
                    "Текст программы",
                    "Программа и методика испытаний",
                    "Протокол испытаний"
                });

            // Стадия 5: внедрение.
            StageProject fifthStage = new StageProject(
                5,
                "Внедрение",
                new HashSet<string>
                {
                    "Акт приёмки-передачи программы",
                    "Формуляр",
                    "Ведомость эксплуатационных документов",
                    "Описание порядка резервного копирования"
                },
                new HashSet<string>
                {
                    "Акт приёмки-передачи программы"
                });

            // Добавляем все стадии в план проекта.
            plan.Stages.Add(firstStage);
            plan.Stages.Add(secondStage);
            plan.Stages.Add(thirdStage);
            plan.Stages.Add(fourthStage);
            plan.Stages.Add(fifthStage);

            // Выводим стадии и их документы.
            foreach (StageProject stage in plan.Stages)
            {
                Console.WriteLine($"{stage.Id}. {stage.Name}");

                Console.WriteLine("Обязательные документы:");

                foreach (string document in stage.RequiredDocuments)
                {
                    Console.WriteLine($"   - {document}");
                }

                Console.WriteLine("Ядро:");

                foreach (string document in stage.CoreDocuments)
                {
                    Console.WriteLine($"   - {document}");
                }

                Console.WriteLine();
            }

            // Контрольный набор для проверки стадии 2.
            HashSet<string> existingDocuments1 = new HashSet<string>
            {
                "Техническое задание",
                "Пояснительная записка эскизного проекта"
            };

            // Контрольный набор для проверки стадии 4.
            HashSet<string> existingDocuments2 = new HashSet<string>
            {
                "Спецификация",
                "Текст программы",
                "Программа и методика испытаний",
                "Протокол испытаний"
            };

            // Отложенные документы для итерационной модели.
            HashSet<string> deferredDocuments = new HashSet<string>();

            // Общий набор уже имеющихся документов.
            HashSet<string> allExistingDocuments = new HashSet<string>();

            // Основной цикл проверки стадий.
            while (true)
            {
                Console.WriteLine(
                    "\nВыберите текущую стадию (от 1 до 5) (0 == exit)");

                // Проверяем корректность ввода номера стадии.
                if (!int.TryParse(Console.ReadLine(), out int stageId))
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                    continue;
                }

                // Выход из программы.
                if (stageId == 0)
                {
                    break;
                }

                // Ищем стадию по номеру.
                StageProject currentStage =
                    plan.Stages.Find(stage => stage.Id == stageId);

                // Проверяем, что стадия существует.
                if (currentStage == null)
                {
                    Console.WriteLine("Стадия с таким номером не найдена!");
                    continue;
                }

                // Выбираем контрольный набор документов для стадии.
                HashSet<string> existingDocuments;

                if (stageId == 2)
                {
                    existingDocuments = existingDocuments1;
                }
                //else if (stageId == 4)
                //{
                //    existingDocuments = existingDocuments2;
                //}
                else
                {
                    existingDocuments = new HashSet<string>();
                }

                // Пополняем общий набор имеющихся документов.
                allExistingDocuments.UnionWith(existingDocuments);

                // Убираем из отложенных документы, которые уже появились.
                deferredDocuments.ExceptWith(allExistingDocuments);

                // Для последней стадии следующей стадии нет.
                if (stageId == 5)
                {
                    Console.WriteLine(
                        "Это последняя стадия — следующей стадии нет.");

                    // Формируем итоговый набор документов внедрения.
                    HashSet<string> finalDocuments =
                        new HashSet<string>(currentStage.RequiredDocuments);

                    // Добавляем ранее отложенные документы.
                    finalDocuments.UnionWith(deferredDocuments);

                    // Убираем уже имеющиеся документы.
                    finalDocuments.ExceptWith(allExistingDocuments);

                    // Проверяем полноту комплекта документов.
                    if (finalDocuments.Count == 0)
                    {
                        Console.WriteLine("Все документы готовы!");
                    }
                    else
                    {
                        Console.WriteLine("Не хватает документов:");

                        foreach (string document in finalDocuments)
                        {
                            Console.WriteLine($"   - {document}");
                        }
                    }

                    continue;
                }

                // Проверка по каскадной модели.
                Console.WriteLine("\nКаскадная модель:");

                if (CascadeCanMoveToNextStage(
                    currentStage, existingDocuments))
                {
                    Console.WriteLine("Переход разрешён!");
                }
                else
                {
                    Console.WriteLine("Переход запрещён!");
                    Console.WriteLine("Недостающие документы:");

                    // Находим недостающие обязательные документы.
                    HashSet<string> missingDocuments =
                        new HashSet<string>(currentStage.RequiredDocuments);

                    missingDocuments.ExceptWith(existingDocuments);

                    foreach (string document in missingDocuments)
                    {
                        Console.WriteLine($"   - {document}");
                    }
                }

                // Проверка по итерационной модели.
                Console.WriteLine("\nИтерационная модель:");

                if (IterationCanMoveToNextStage(
                    currentStage, existingDocuments))
                {
                    Console.WriteLine("Переход разрешён!");

                    // Находим документы, которые можно отложить.
                    HashSet<string> currentDeferredDocuments =
                        new HashSet<string>(currentStage.RequiredDocuments);

                    currentDeferredDocuments.ExceptWith(existingDocuments);

                    // Сохраняем отложенные документы.
                    deferredDocuments.UnionWith(currentDeferredDocuments);

                    Console.WriteLine(
                        $"Количество отложенных документов: {deferredDocuments.Count}");

                    foreach (string document in deferredDocuments)
                    {
                        Console.WriteLine($"   - {document}");
                    }
                }
                else
                {
                    Console.WriteLine("Переход запрещён!");
                    Console.WriteLine("Недостающие документы ядра:");

                    // Находим недостающие документы ядра.
                    HashSet<string> missingDocuments =
                        new HashSet<string>(currentStage.CoreDocuments);

                    missingDocuments.ExceptWith(existingDocuments);

                    foreach (string document in missingDocuments)
                    {
                        Console.WriteLine($"   - {document}");
                    }
                }
            }
        }

        // Итерационная модель: нужны все документы ядра.
        static bool IterationCanMoveToNextStage(
            StageProject stage,
            HashSet<string> existingDocuments) => stage.CoreDocuments.IsSubsetOf(existingDocuments);

        // Каскадная модель: нужны все обязательные документы.
        static bool CascadeCanMoveToNextStage(
            StageProject stage,
            HashSet<string> existingDocuments) => stage.RequiredDocuments.IsSubsetOf(existingDocuments);
    }
}