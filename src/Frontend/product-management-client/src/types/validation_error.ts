export default class ValidationError {
    private message: string; 
    formattedMessage: string;

    constructor(message: string) {
        this.message = message;
        this.formattedMessage = this.FormatMessage();
    }

    private FormatMessage() : string
    {
        return this.message
        .split("\n")
        .map(line => {
            const match = line.match(/--\s*(.*?):/);
            return match ? line.replace(match[0], "").trim() : line.trim();
        })
        .map(line => line.replace("Validation failed:", "")
                         .replace("Severity: Error", "")
                         .trim())
        .filter(line => line.length > 0)
        .join("\n");
    } 
}   