﻿namespace ProductManagement.Common.Exceptions;

public class InternalServerErrorException(string message) : Exception(message);