//
//  Tracker.h
//  XTestApp
//
//  Created by Woncheol Heo on 2019. 4. 3..
//  Copyright © 2019년 wisetracker. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <WebKit/WebKit.h>
#import "XIdentify.h"
#import "XEvent.h"
#import "XConversion.h"
#import "XRevenue.h"

NS_ASSUME_NONNULL_BEGIN

@interface XTracker : NSObject

+ (XTracker *)sharedInstance;
- (void)initialization;
- (void)setUserId:(NSString *)userId;
- (void)setDeepLink:(NSString *)referrer;
- (void)setPushId:(NSString *)pid;
- (void)logEvent:(XEvent *)event;
- (void)logEventWithDictionary:(NSDictionary *)event;
- (void)userIdentify:(XIdentify *)identify;
- (void)userIdentifyWithDictionary:(NSDictionary *)identify;
- (void)groupIdentify:(NSString *)key value:(NSString *)value identify:(XIdentify *)identify;
- (void)groupIdentifyWithDictionary:(NSDictionary *)groups groupProperties:(NSDictionary *)groupProperties;
- (void)logConversion:(XConversion *)conversion;
- (void)logConversionWithDictionary:(NSDictionary *)conversion;
- (void)logRevenue:(XRevenue *)revenue;
- (void)logRevenueWithDictionary:(NSDictionary *)revenue;
- (void)changeKeyInSessionDataWith:(NSString *)oldKey newKey:(NSString *)newKey;
- (void)setJavascriptInjectionInWebView:(UIWebView *)webView;
- (void)setJavascriptInjectionInWkWebView:(WKWebView *)wkWebView;
@end

NS_ASSUME_NONNULL_END
