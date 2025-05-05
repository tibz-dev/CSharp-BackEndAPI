document.getElementById("callApi").addEventListener("click", async () => {
    try {
      const response = await fetch("https://localhost:7180/api/message");
      const text = await response.text();
      document.getElementById("result").textContent = text;
    } catch (err) {
      document.getElementById("result").textContent = "Error: " + err;
    }
  });
  