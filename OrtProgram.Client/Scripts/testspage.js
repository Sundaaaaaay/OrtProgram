const apiUrl = "https://localhost:44340/ort/tests/getall";

// Функция для получения тестов с сервера
function fetchTests() {
  fetch(apiUrl)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      // Вызов функции для отображения тестов
      displayTests(data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
      // Показать сообщение об ошибке
      document.getElementById("error-message").classList.remove("d-none");
    })
    .finally(() => {
      // Скрыть индикатор загрузки
      document.getElementById("loader").style.display = "none";
    });
}

// Функция для отображения тестов
function displayTests(tests) {
  const testList = document.getElementById("test-list");

  // Очистка списка перед обновлением
  testList.innerHTML = "";

  // Итерация по тестам и создание элементов списка
  tests.forEach((test) => {
    console.log(test);
    const listItem = document.createElement("li");
    listItem.classList.add("list-group-item");

    // Создаем ссылку для перехода к тесту
    const testLink = document.createElement("a");
    testLink.href = `testpage.html?testId=${test.id}`; // передаем id через URL
    testLink.textContent = `${test.testType}`; // Название теста
    testLink.classList.add("text-decoration-none", "text-dark");

    // Форматируем содержимое элемента списка
    listItem.innerHTML = `
      <strong>Test Name:</strong> ${testLink.outerHTML} <br>
      <strong>ID:</strong> ${test.id} <br>
      <strong>Description:</strong> ${test.description} <br>
      <strong>Questions:</strong> ${test.questionsAmount}
    `;

    testList.appendChild(listItem);
  });
}

// Вызов функции для получения и отображения тестов при загрузке страницы
window.onload = fetchTests;
