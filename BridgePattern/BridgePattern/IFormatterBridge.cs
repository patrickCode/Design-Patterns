namespace BridgePattern
{
    /*
     * Abstraction - Document is the abstraction.
     * Refined Abstraction - Book / Paper /  FAQ
     * Implementor - Formatter (in order to implement a Document some formatting is required). The formatter should not be tied with the type of formatter. So eveny book here can have it's own formatter.
     *      We don't wan't event Document to have a specific type of implementation of Formatter, i.e., book can have a different formatter, FAQ can have a different one and Term Paper can have it's own formatter.
     * ConcreteImplementor - StandardFormatter.
     * So every concrete document (implementation) can have it's own formatter. Hence the implementor (Book) is not coupled with the abstraction (Manuscript), i.e., Book can have it's own implementor. This is possible by having the IFormatter as a Bridge.
     * For handling different types of formatting there was no need to create an hierarchy of classes. Without formatters for having a formatting applied (like Uppercase formatting) we would have had to create a UpperCaseBook inherited from Book class. Hence for any new type of formatting we would have had to keep creating new classes and that would increase the complexity of the hierarchy.
     */
    public interface IFormatterBridge
    {
        string Format(string format, string value);
    }
}