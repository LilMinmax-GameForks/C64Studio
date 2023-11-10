﻿using System;
using System.Collections.Generic;
using System.Text;
using static Tiny64.Opcode;

namespace Tiny64
{
  public partial class Processor
  {
    public static Processor Create68000()
    {
      var  sys = new Processor( "Motorola 68000" );

      sys.LittleEndian = false;


      var aRegisters = new List<ValidValue>() { new ValidValue( "a0", 0 ),
                                       new ValidValue( "a1", 1 ),
                                       new ValidValue( "a2", 2 ),
                                       new ValidValue( "a3", 3 ),
                                       new ValidValue( "a4", 4 ),
                                       new ValidValue( "a5", 5 ),
                                       new ValidValue( "a6", 6 ),
                                       new ValidValue( "a7", 7 ),
                                       new ValidValue( "sp", 7 ) };
      var dRegisters = new List<ValidValue>() { new ValidValue( "d0", 0 ),
                                       new ValidValue( "d1", 1 ),
                                       new ValidValue( "d2", 2 ),
                                       new ValidValue( "d3", 3 ),
                                       new ValidValue( "d4", 4 ),
                                       new ValidValue( "d5", 5 ),
                                       new ValidValue( "d6", 6 ),
                                       new ValidValue( "d7", 7 ) };
      var dRegistersDotW = new List<ValidValue>() { new ValidValue( "d0.w", 0 ),
                                       new ValidValue( "d1.w", 1 ),
                                       new ValidValue( "d2.w", 2 ),
                                       new ValidValue( "d3.w", 3 ),
                                       new ValidValue( "d4.w", 4 ),
                                       new ValidValue( "d5.w", 5 ),
                                       new ValidValue( "d6.w", 6 ),
                                       new ValidValue( "d7.w", 7 ) };
      var quickAdd = new List<ValidValue>() { new ValidValue( "1", 1 ),
                                       new ValidValue( "2", 2 ),
                                       new ValidValue( "3", 3 ),
                                       new ValidValue( "4", 4 ),
                                       new ValidValue( "5", 5 ),
                                       new ValidValue( "6", 6 ),
                                       new ValidValue( "7", 7 ),
                                       new ValidValue( "8", 0 ) };
      var bits = new List<ValidValue>() { new ValidValue( "0", 0 ),
                                       new ValidValue( "1", 1 ),
                                       new ValidValue( "2", 2 ),
                                       new ValidValue( "3", 3 ),
                                       new ValidValue( "4", 4 ),
                                       new ValidValue( "5", 5 ),
                                       new ValidValue( "6", 6 ),
                                       new ValidValue( "7", 7 ) };
      var immediatePrefix = new List<ValidValue>() { new ValidValue( "#" ) };
      var comma = new List<ValidValue>() { new ValidValue( "," ) };
      var empty = new List<ValidValue>();
      var sr = new List<ValidValue>() { new ValidValue( "sr" ) };

      var openingParenthesis = new List<ValidValue>() { new ValidValue( "(" ) };
      var openingParenthesisMinus = new List<ValidValue>() { new ValidValue( "-" ), new ValidValue( "(" ) };
      var closingParenthesisDotW = new List<ValidValue>() { new ValidValue( ")" ), new ValidValue( ".w" ) };
      var closingParenthesisDotL = new List<ValidValue>() { new ValidValue( ")" ), new ValidValue( ".l" ) };
      var closingParenthesis = new List<ValidValue>() { new ValidValue( ")" ) };
      var closingParenthesisPlus = new List<ValidValue>() { new ValidValue( ")" ), new ValidValue( "+" ) };

      // adda.w
      sys.AddOpcode( "adda.w", 0xD0FC0000, 2, AddressingType.IMMEDIATE_16BIT, 0 )          // adda.w #$20, a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      sys.AddOpcode( "adda.w", 0xD0F80000, 2, AddressingType.INDIRECT, 0 )              // adda.w ($1234).w, a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      sys.AddOpcode( "adda.l", 0xD1F9000000u, 3, AddressingType.INDIRECT, 0 )              // adda.l ($12345678).l, a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_24BIT, openingParenthesis, closingParenthesisDotL, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 33 ) } );

      sys.AddOpcode( "adda.w", 0xD0C0, 0, AddressingType.IMPLICIT, 0 )                     // adda.w d0,a2
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 3 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 9 ) } );

      sys.AddOpcode( "adda.w", 0xD0D0, 0, AddressingType.INDIRECT, 0 )                     // adda.w (a0),a1
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 9 ) } );

      sys.AddOpcode( "adda.w", 0xD0E0, 0, AddressingType.INDIRECT, 0 )                     // adda.w -(a0),a1
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 9 ) } );

      sys.AddOpcode( "adda.w", 0xD0E80000, 2, AddressingType.INDIRECT, 0 )                      // adda.w $10(a0), a1
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_16BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      sys.AddOpcode( "adda.w", 0xD0D8, 0, AddressingType.INDIRECT, 0 )                          // adda.w (a0)+,a1
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 9 ) } );

      sys.AddOpcode( "adda.w", 0xD0F00000, 0, AddressingType.INDIRECT, 0 )                      // adda.w (a3,d1.w), a3
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( comma ),
              new ValidValueGroup( dRegistersDotW, OpcodePartialExpression.TOKEN_LIST, 12 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      sys.AddOpcode( "adda.w", 0xD0F00000, 2, AddressingType.INDIRECT, 0 )                      // adda.w (a3,d1.w), a3
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( comma ),
              new ValidValueGroup( dRegistersDotW, OpcodePartialExpression.TOKEN_LIST, 12 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      // addi
      sys.AddOpcode( "addi.b", 0x06000000, 1, AddressingType.IMMEDIATE_8BIT, 0 )          // addi.w #$20, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "addi.l", 0x068000000000, 4, AddressingType.IMMEDIATE_32BIT, 0 )          // addi.l #$12345678, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 32 ) } );

      sys.AddOpcode( "addi.w", 0x067800000000, 4, AddressingType.IMMEDIATE_16BIT, 0 )             // addi.w #$1234, ($1234).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "addi.w", 0x0679000000000000, 4, AddressingType.IMMEDIATE_16BIT, 0 )             // addi.w #$1234, ($12345678).l
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 32 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, openingParenthesis, closingParenthesisDotL, 0 ) } );

      sys.AddOpcode( "addi.l", 0x06B8000000000000, 4, AddressingType.IMMEDIATE_16BIT, 0 )             // addi.l #$00000001, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "addi.b", 0x06100000, 2, AddressingType.IMMEDIATE_8BIT, 0 )             // addi.b #$01, (a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "addi.w", 0x066800000000, 4, AddressingType.INDIRECT, 0 )               // adda.i #$12, $34(a1)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "addi.l", 0x06A8000000000000, 4, AddressingType.INDIRECT, 0 )              // addi.l #$00000001, $FF(a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 48 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "addi.b", 0x06180000, 2, AddressingType.IMPLICIT, 0 )              // addi.b #$01, (a0)+
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 16 ) } );

      sys.AddOpcode( "addi.b", 0x06280000, 2, AddressingType.IMPLICIT, 0 )              // addi.b #$01, -(a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "addq.b", 0x50380000, 2, AddressingType.IMMEDIATE_16BIT, 0 )        // addq.b #1, ($0001).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 25 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW ) } );

      sys.AddOpcode( "addq.b", 0x5000, 0, AddressingType.IMMEDIATE_8BIT, 0 )        // addq.b #1, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters ) } );

      sys.AddOpcode( "addq.w", 0x5040, 0, AddressingType.IMMEDIATE_8BIT, 0 )        // addq.w #1, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters ) } );

      sys.AddOpcode( "addq.w", 0x5050, 0, AddressingType.IMMEDIATE_8BIT, 0 )        // addq.w #1, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "addq.w", 0x5060, 1, AddressingType.IMMEDIATE_8BIT, 0 )        // addq.w #1, -(a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "addq.b", 0x50280000, 2, AddressingType.IMPLICIT, 0 )          // addq.b #1, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "addq.w", 0x50680000, 2, AddressingType.IMPLICIT, 0 )          // addq.w #1, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "addq.w", 0x5058, 0, AddressingType.IMMEDIATE_8BIT, 0 )        // addq.w #1, (a0)+
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ) } );

      sys.AddOpcode( "addq.w", 0x5048, 0, AddressingType.IMMEDIATE_8BIT, 0 )        // addq.w #1, a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters ) } );

      sys.AddOpcode( "addq.l", 0x5088, 0, AddressingType.IMMEDIATE_8BIT, 0 )        // addq.l #1, a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, immediatePrefix, quickAdd, empty, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters ) } );

      sys.AddOpcode( "add.w", 0xD040, 0, AddressingType.IMPLICIT, 0 )               // add.w d0,d1
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "add.w", 0xD0780000, 2, AddressingType.INDIRECT, 0 )           // add.w ($1234).w, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "add.w", 0xD050, 0, AddressingType.INDIRECT, 0 )               // add.w (a0), d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "add.b", 0xD0280000, 2, AddressingType.IMMEDIATE_8BIT, 0 )                      // add.b $01(a0),d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "add.w", 0xD060, 0, AddressingType.IMPLICIT, 0 )                     // add.w -(a0),d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "add.w", 0xD0680000, 2, AddressingType.IMMEDIATE_8BIT, 0 )                      // add.w $01(a0),d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "add.w", 0xD058, 0, AddressingType.IMPLICIT, 0 )                     // add.w (a0)+,d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "add.l", 0xD088, 0, AddressingType.IMPLICIT, 0 )               // add.l a0,d1
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "add.w", 0xD1780000, 2, AddressingType.INDIRECT, 0 )           // add.w d0, ($0001).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "add.w", 0xD150, 0, AddressingType.INDIRECT, 0 )               // add.w d0,(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "add.w", 0xD168, 0, AddressingType.INDIRECT, 0 )               // add.w d0,-(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "add.w", 0xD1680000, 2, AddressingType.INDIRECT, 0 )          // add.w d0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "add.l", 0xD1A80000, 2, AddressingType.INDIRECT, 0 )          // add.l d0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "add.l", 0xD198, 0, AddressingType.INDIRECT, 0 )               // add.l d0,(a7)+
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ) } );

      
      sys.AddOpcode( "andi.b", 0x02000000, 1, AddressingType.IMMEDIATE_8BIT, 0 )    // andi.b #$01, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "andi.w", 0x02400000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // andi.w #$1234, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "andi.l", 0x028000000000, 4, AddressingType.IMMEDIATE_32BIT, 0 )    // andi.l #$00000001, d7
      .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 32 ) } );

      sys.AddOpcode( "andi.b", 0x023800000000, 4, AddressingType.IMMEDIATE_16BIT, 0 )             // andi.b #$12, ($1234).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "andi.w", 0x027800000000, 4, AddressingType.IMMEDIATE_16BIT, 0 )             // andi.w #$1234, ($1234).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "andi.b", 0x02100000, 2, AddressingType.IMMEDIATE_8BIT, 0 )             // andi.b #$01, (a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "andi.b", 0x02200000, 2, AddressingType.IMMEDIATE_8BIT, 0 )             // andi.b #$01, -(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "andi.b", 0x022800000000, 4, AddressingType.IMMEDIATE_8BIT, 0 )               // andi.i #$12, $34(a1)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "andi.b", 0x02180000, 2, AddressingType.IMMEDIATE_8BIT, 0 )             // andi.b #$01, (a7)+
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 16 ) } );

      sys.AddOpcode( "andi.w", 0x026800000000, 4, AddressingType.IMMEDIATE_16BIT, 0 )               // andi.i #$1234, $34(a1)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "andi.b", 0x023000000000, 4, AddressingType.IMMEDIATE_8BIT, 0 )         // andi.b #$01,(a0,d7.w)       ;02 37 00 01 70 00
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( comma ),
              new ValidValueGroup( dRegistersDotW, OpcodePartialExpression.TOKEN_LIST, 12 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "andi.w", 0x027000000000, 4, AddressingType.IMMEDIATE_16BIT, 0 )    // andi.w #$0001, $FF(a0,d7.w)           ;02 70 00 01 70 FF
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( comma ),
              new ValidValueGroup( dRegistersDotW, OpcodePartialExpression.TOKEN_LIST, 12 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "andi", 0x027C0000, 2, AddressingType.IMMEDIATE_16BIT, 0 )     // andi #$0001,sr      ;02 7C 00 01
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.TOKEN_LIST, sr ) } );

      sys.AddOpcode( "and.b", 0xC000, 0, AddressingType.IMPLICIT, 0 )            // and.b d0,d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "and.b", 0xC0380000, 2, AddressingType.INDIRECT, 0 )       // and.b ($0001).w, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "and.w", 0xC0780000, 2, AddressingType.INDIRECT, 0 )       // and.w ($0001).w, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "and.w", 0xC060, 0, AddressingType.INDIRECT, 0 )         // and.w -(a7), d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "and.w", 0xC050, 0, AddressingType.INDIRECT, 0 )           // and.w (a7), d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "and.w", 0xC0680000, 2, AddressingType.INDIRECT, 0 )      // and.w $01(a7), d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );
      
      sys.AddOpcode( "and.w", 0xC058, 0, AddressingType.INDIRECT, 0 )         // and.w (a7)+, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "and.w", 0xC1780000, 2, AddressingType.INDIRECT, 0 )     // and.w d0, ($0001).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "and.w", 0xC150, 0, AddressingType.INDIRECT, 0 )         // and.w d0,(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "and.w", 0xC160, 0, AddressingType.INDIRECT, 0 )         // and.w d0,-(a7)            ;C1 67
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "and.w", 0xC1680000, 2, AddressingType.INDIRECT, 0 )     // and.w d0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "and.w", 0xC158, 0, AddressingType.INDIRECT, 0 )         // and.w d0,(a7)+            ;C1 5F
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ) } );

      sys.AddOpcode( "bra.s", 0x6000, 1, AddressingType.RELATIVE, 0 )        // bra.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bra.w", 0x60000000, 1, AddressingType.RELATIVE_16, 0 ) // bra.w label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "bsr.s", 0x6100, 1, AddressingType.RELATIVE, 0 )        // bsr.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bhi.s", 0x6200, 1, AddressingType.RELATIVE, 0 )        // bhi.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bls.s", 0x6300, 1, AddressingType.RELATIVE, 0 )        // bls.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bcc.s", 0x6400, 1, AddressingType.RELATIVE, 0 )        // bcc.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bcs.s", 0x6500, 1, AddressingType.RELATIVE, 0 )        // bcs.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bne.s", 0x6600, 1, AddressingType.RELATIVE, 0 )        // bne.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "beq.s", 0x6700, 1, AddressingType.RELATIVE, 0 )        // beq.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bvc.s", 0x6800, 1, AddressingType.RELATIVE, 0 )        // bvc.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bvs.s", 0x6900, 1, AddressingType.RELATIVE, 0 )        // bvs.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bpl.s", 0x6A00, 1, AddressingType.RELATIVE, 0 )        // bpl.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bmi.s", 0x6B00, 1, AddressingType.RELATIVE, 0 )        // bmi.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bge.s", 0x6C00, 1, AddressingType.RELATIVE, 0 )        // bge.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "blt.s", 0x6D00, 1, AddressingType.RELATIVE, 0 )        // blt.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "bgt.s", 0x6E00, 1, AddressingType.RELATIVE, 0 )        // bgt.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "ble.s", 0x6F00, 1, AddressingType.RELATIVE, 0 )        // ble.s label
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_8BIT_RELATIVE ) } );

      sys.AddOpcode( "btst", 0x083800000000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // btst #0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "btst", 0x08000000, 2, AddressingType.INDIRECT, 0 )    // btst #0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "btst", 0x08100000, 2, AddressingType.IMMEDIATE_8BIT, 0 )    // btst #0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "btst", 0x082800000000, 2, AddressingType.INDIRECT, 0 )          // btst #0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "btst", 0x01380000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // btst d0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "btst", 0x0100, 0, AddressingType.IMPLICIT, 0 )    // btst d0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ) } );

      sys.AddOpcode( "btst", 0x0110, 0, AddressingType.INDIRECT, 0 )    // btst d0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "btst", 0x01280000, 2, AddressingType.IMMEDIATE_8BIT, 0 )          // btst d0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "bclr", 0x08B800000000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // bclr #0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "bclr", 0x08800000, 2, AddressingType.INDIRECT, 0 )    // bclr #0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "bclr", 0x08900000, 2, AddressingType.IMMEDIATE_8BIT, 0 )    // bclr #0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "bclr", 0x08A800000000, 2, AddressingType.INDIRECT, 0 )          // bclr #0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "bclr", 0x01B80000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // bclr d0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "bclr", 0x0180, 0, AddressingType.IMPLICIT, 0 )    // bclr d0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ) } );

      sys.AddOpcode( "bclr", 0x0190, 0, AddressingType.INDIRECT, 0 )    // bclr d0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "bclr", 0x01A80000, 2, AddressingType.IMMEDIATE_8BIT, 0 )          // bclr d0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );


      sys.AddOpcode( "bset", 0x08F800000000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // bset #0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "bset", 0x08C00000, 2, AddressingType.INDIRECT, 0 )    // bset #0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "bset", 0x08D00000, 2, AddressingType.IMMEDIATE_8BIT, 0 )    // bset #0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "bset", 0x08E800000000, 2, AddressingType.INDIRECT, 0 )          // bset #0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "bset", 0x01F80000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // bset d0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "bset", 0x01C0, 0, AddressingType.IMPLICIT, 0 )    // bset d0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ) } );

      sys.AddOpcode( "bset", 0x01D0, 0, AddressingType.INDIRECT, 0 )    // bset d0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "bset", 0x01E80000, 2, AddressingType.IMMEDIATE_8BIT, 0 )          // bset d0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "bchg", 0x087800000000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // bchg #0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "bchg", 0x08400000, 2, AddressingType.INDIRECT, 0 )    // bchg #0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "bchg", 0x08500000, 2, AddressingType.IMMEDIATE_8BIT, 0 )    // bchg #0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "bchg", 0x086800000000, 2, AddressingType.INDIRECT, 0 )          // bchg #0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, bits, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "bchg", 0x01780000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // bchg d0, ($FF00).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "bchg", 0x0140, 0, AddressingType.IMPLICIT, 0 )    // bchg d0, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ) } );

      sys.AddOpcode( "bchg", 0x0150, 0, AddressingType.INDIRECT, 0 )    // bchg d0, (a0)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "bchg", 0x01680000, 2, AddressingType.IMMEDIATE_8BIT, 0 )          // bchg d0, $01(a7)
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "clr.w", 0x4240, 0, AddressingType.IMPLICIT, 0 )    // clr.w d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ) } );

      sys.AddOpcode( "clr.l", 0x4280, 0, AddressingType.IMPLICIT, 0 )    // clr.l d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ) } );

      sys.AddOpcode( "clr.w", 0x42780000, 2, AddressingType.INDIRECT, 0 )    // clr.w ($1234).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "clr.b", 0x42280000, 2, AddressingType.INDIRECT, 0 )    // clr.b $01( a7 )
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "clr.w", 0x42680000, 2, AddressingType.INDIRECT, 0 )    // clr.w $01( a7 )
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "clr.l", 0x4290, 0, AddressingType.IMPLICIT, 0 )    // clr.l (a7)   
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "clr.l", 0x4298, 0, AddressingType.IMPLICIT, 0 )    // clr.l (a7)+ 
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ) } );

      sys.AddOpcode( "clr.l", 0x42A0, 0, AddressingType.IMPLICIT, 0 )    // clr.l -(a7)   
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ) } );

      sys.AddOpcode( "clr.l", 0x42A80000, 2, AddressingType.INDIRECT, 0 )    // clr.l $12(a7)   
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "cmpa.w", 0xB0FC0000, 2, AddressingType.IMMEDIATE_16BIT, 0 )     // cmpa.w #$0001,a7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      sys.AddOpcode( "cmpa.l", 0xB1FC00000000, 4, AddressingType.IMMEDIATE_32BIT, 0 ) // cmpa.l #$00000001,a7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 41 ) } );

      sys.AddOpcode( "cmpa.l", 0xB1F80000, 2, AddressingType.IMMEDIATE_32BIT, 0 )   // cmpa.l ($0001).w,a7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      sys.AddOpcode( "cmpa.w", 0xB0C0, 0, AddressingType.IMPLICIT, 0 )    // cmpa.w d7, a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 9 ) } );



      sys.AddOpcode( "cmpa.l", 0xB1C8, 0, AddressingType.IMPLICIT, 0 )    // cmpa.w a7, a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 9 ) } );

      sys.AddOpcode( "cmpa.l", 0xB1D8, 0, AddressingType.IMPLICIT, 0 )    // cmpa.l (a7), a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 9 ) } );

      sys.AddOpcode( "cmpa.l", 0xB1E80000, 2, AddressingType.INDIRECT, 0 )    // cmpa.l $01(a7), a0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, aRegisters, 25 ) } );

      sys.AddOpcode( "cmpi.b", 0x0C3800000000, 2, AddressingType.IMMEDIATE_8BIT, 0 )     // cmpi.b #$12, ($1234).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "cmpi.w", 0x0C7800000000, 2, AddressingType.IMMEDIATE_16BIT, 0 )    // cmpi.w #$1234, ($1234).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "cmpi.l", 0x0CB8000000000000, 4, AddressingType.IMMEDIATE_32BIT, 0 )    // cmpi.l #$12345678, ($1234).w
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ) } );

      sys.AddOpcode( "cmpi.w", 0x0C79000000000000, 4, AddressingType.IMMEDIATE_8BIT, 0 )    // cmpi.l #$12, ($12345678).l
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 32 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, openingParenthesis, closingParenthesisDotL, 0 ) } );

      sys.AddOpcode( "cmpi.b", 0x0C000000, 2, AddressingType.IMMEDIATE_8BIT, 0 )    // cmpi.b #$01, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "cmpi.w", 0x0C400000, 2, AddressingType.IMMEDIATE_16BIT, 0 )   // cmpi.w #$1234, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ) } );

      sys.AddOpcode( "cmpi.l", 0x0C8000000000, 4, AddressingType.IMMEDIATE_32BIT, 0 )   // cmpi.l #$12345678, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_32BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 32 ) } );

      sys.AddOpcode( "cmpi.b", 0x0C100000, 2, AddressingType.IMMEDIATE_8BIT, 0 )    // cmpi.b #$01, (a7)           ;0C 17 00 01
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "cmpi.b", 0x0C200000, 2, AddressingType.IMMEDIATE_8BIT, 0 )  // cmpi.b #$01, -(a7)          ;0C 1F 00 01
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 16 ) } );

      sys.AddOpcode( "cmpi.b", 0x0C2800000000, 2, AddressingType.INDIRECT, 0 )    // cmpi.b #$01, $FF(a7)           ;0C 2F 00 01 00 FF
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "cmpi.w", 0x0C6800000000, 2, AddressingType.INDIRECT, 0 )    // cmpi.w #$0001, $FF(a7)         ;0C 6F 00 01 00 FF
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, immediatePrefix, empty, 16 ),
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 32 ),
              new ValidValueGroup( closingParenthesis )
            }
          } } );

      sys.AddOpcode( "cmpi.b", 0x0C180000, 2, AddressingType.IMMEDIATE_8BIT, 0 )  // cmpi.b #$01, (a7)+          ;0C 1F 00 01
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_8BIT, immediatePrefix, empty, 0 ),
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 16 ) } );


      sys.AddOpcode( "cmp.b", 0xB000, 0, AddressingType.IMPLICIT, 0 )    // cmp.b d0, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "cmp.l", 0xB080, 0, AddressingType.IMPLICIT, 0 )    // cmp.l d0, d7
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "cmp.w", 0xB0780000, 2, AddressingType.INDIRECT, 0 )   // cmp.w ($0001).w, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "cmp.l", 0xB0B80000, 2, AddressingType.INDIRECT, 0 )   // cmp.l ($0001).w, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_EXPRESSION_16BIT, openingParenthesis, closingParenthesisDotW, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "cmp.w", 0xB050, 0, AddressingType.INDIRECT, 0 )       // cmp.w (a7), d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "cmp.w", 0xB058, 0, AddressingType.INDIRECT, 0 )       // cmp.w (a7)+, d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesis, aRegisters, closingParenthesisPlus, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "cmp.b", 0xB020, 0, AddressingType.INDIRECT, 0 )       // cmp.b -(a7), d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.ENCAPSULATED_VALUE_FROM_LIST, openingParenthesisMinus, aRegisters, closingParenthesis, 0 ),
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 9 ) } );

      sys.AddOpcode( "cmp.b", 0xB0280000, 2, AddressingType.IMMEDIATE_8BIT, 0 )   // cmp.b $01(a0),d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "cmp.w", 0xB0680000, 2, AddressingType.IMMEDIATE_8BIT, 0 )   // cmp.w $01(a0),d0
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.COMPLEX )
          {
            ValidValues = new List<ValidValueGroup>()
            {
              new ValidValueGroup( empty, OpcodePartialExpression.EXPRESSION_8BIT ),
              new ValidValueGroup( openingParenthesis ),
              new ValidValueGroup( aRegisters, OpcodePartialExpression.TOKEN_LIST, 16 ),
              new ValidValueGroup( closingParenthesis )
            }
          },
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 25 ) } );

      sys.AddOpcode( "dbt", 0x50C80000, 2, AddressingType.RELATIVE_16, 0 )    // dbt d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbf", 0x51C80000, 2, AddressingType.RELATIVE_16, 0 )    // dbf d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbhi", 0x52C80000, 2, AddressingType.RELATIVE_16, 0 )    // dbhi d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbls", 0x53C80000, 2, AddressingType.RELATIVE_16, 0 )    // dbls d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbcc", 0x54C80000, 2, AddressingType.RELATIVE_16, 0 )    // dbcc d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbcs", 0x55C80000, 2, AddressingType.RELATIVE_16, 0 )    // dbcs d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbne", 0x56C80000, 2, AddressingType.RELATIVE_16, 0 )   // dbne d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbeq", 0x57C80000, 2, AddressingType.RELATIVE_16, 0 )   // dbeq d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbvc", 0x58C80000, 2, AddressingType.RELATIVE_16, 0 )   // dbvc d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbvs", 0x59C80000, 2, AddressingType.RELATIVE_16, 0 )   // dbvs d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbpl", 0x5AC80000, 2, AddressingType.RELATIVE_16, 0 )   // dbpl d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbmi", 0x5BC80000, 2, AddressingType.RELATIVE_16, 0 )   // dbmi d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbge", 0x5CC80000, 2, AddressingType.RELATIVE_16, 0 )   // dbg d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dblt", 0x5DC80000, 2, AddressingType.RELATIVE_16, 0 )   // dblt d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dbgt", 0x5EC80000, 2, AddressingType.RELATIVE_16, 0 )   // dbgt d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );

      sys.AddOpcode( "dble", 0x5FC80000, 2, AddressingType.RELATIVE_16, 0 )   // dble d0, $1234
        .ParserExpressions.AddRange( new List<OpcodeExpression>() {
          new OpcodeExpression( OpcodePartialExpression.VALUE_FROM_LIST, dRegisters, 16 ),
          new OpcodeExpression( OpcodePartialExpression.EXPRESSION_16BIT_RELATIVE ) } );


      return sys;
    }



  }
}
