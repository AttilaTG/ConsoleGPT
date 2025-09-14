# ConsoleGPT – TinyLlama Console App 🤖

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-✓-blue)](https://docker.com)
[![Ollama](https://img.shields.io/badge/Ollama-✓-orange)](https://ollama.ai)

A .NET 8 console application for interacting with the llama3.2:1b model via HTTP. Fully dockerized with Ollama Server included, ready to run with minimal configuration.

## ✨ Features

- 💬 Interactive chat with llama3.2:1b model from console
- 🐳 Fully containerized: Ollama Server + .NET app in Docker
- ⚡ Minimal setup - just Docker required
- 💾 Persistent model storage with Docker volumes

---

## 🚀 Quick Start

### 1. Clone and build
```bash
git clone https://github.com/AttilaTG/ConsoleGPT.git
cd ConsoleGPT
docker compose up --build -d
```

### 2. Download the model (in a new terminal)

> ⚠️ Only needed the first time you run the application. The model is stored in the Docker volume for future use.

```bash
docker exec ollama-server ollama pull llama3.2:1b
```

### 3. Start chatting!
```bash
docker compose run --rm consolegpt
```

### 4. Stop and Cleanup

To stop the containers and remove the network:

```bash
docker compose down
```

---

## 📋 Usage

- Type your message and press **Enter**
- The app will respond interactively from TinyLlama
- Type `exit` to quit the chat
- Conversation history is maintained during the session

---

## 🔌 Plugins

ConsoleGPT supports **Semantic Kernel plugins** that extend the model’s capabilities by connecting it to external APIs and services.

### 🌦 Weather Plugin

The first included plugin is the **Weather Plugin**, which integrates with [WeatherAPI](https://www.weatherapi.com/) to fetch real-time weather information.
When you ask about the weather in a specific city, the model can automatically call this plugin and answer based on live data.

#### ⚙️ Configuration

The plugin requires a WeatherAPI key.
Add your configuration in **`settings.json`**:

```json
{
  "Plugins": {
    "WeatherPlugin": {
      "CurrentWeatherEndpoint": "http://api.weatherapi.com/v1/current.json?q={0}&key={1}",
      "ApiKey": "Your_Api_Key"
    }
  }
}
```

> Replace `"Your_Api_Key"` with your actual API key from [WeatherAPI](https://www.weatherapi.com/).

#### 🗣 Example usage

```text
> What's the weather in Madrid?
It's currently 22°C in Madrid with light rain 🌧️
```

---

## 🐳 Docker Compose Services

| Service | Description 
|---------|-------------
| **ollama-server** | Ollama server with model storage
| **consolegpt** | .NET 8 console chat app

---

## 🔧 Troubleshooting

### Model not downloading?
```bash
docker exec ollama-server ollama pull llama3.2:1b
docker exec ollama-server ollama list
```

### Connection issues?
```bash
# Test Ollama API
docker exec ollama-server curl http://localhost:11434/api/tags

# Check containers
docker ps
docker logs ollama-server
```
---

## 📝 Notes

- First model download may take 2-3 minutes
- Models are stored in `ollama_data` Docker volume
- Console app auto-reconnects if Ollama Server restarts

---

## 📄 License

MIT License - see LICENSE file for details