﻿namespace ProductManagement.Common.Exceptions;

public class ForbiddenException(string message) : Exception(message);